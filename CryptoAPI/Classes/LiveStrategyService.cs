using CryptoAPI.Classes.Helpers;
using CryptoCore.Classes;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoAPI.Classes
{
    public class LiveStrategyService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IApplicationLifetime _appLifetime;

        public LiveStrategyService(
            IServiceProvider serviceProvider,
            IApplicationLifetime appLifetime)
        {
            _serviceProvider = serviceProvider;
            _appLifetime = appLifetime;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return RunAsync(stoppingToken);
        }


        private static Dictionary<int, MarketInfo> AllMarkets = null;
        private static Dictionary<int, UserInfo> AllUsers = null;
        private static Dictionary<int, Context> AllContexts = null; // <strategyid,context>

        private static void RefreshUsers()
        {
            AllUsers = new Dictionary<int, UserInfo>();


            var d = DBHelper.GetDataTable("SELECT * FROM Users");

            foreach (DataRow item in d.Rows)
            {
                var m = new UserInfo(item);

                AllUsers[m.Id] = m;
            }
        }

        private static void RefreshMarkets()
        {
            AllMarkets = new Dictionary<int, MarketInfo>();


            var d = DBHelper.GetDataTable("SELECT * FROM Markets");

            foreach (DataRow item in d.Rows)
            {
                var m = new MarketInfo(item);

                AllMarkets[m.Id] = m;
            }
        }

        private async Task RunAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                // get live orders


                if (AllMarkets == null)
                {
                    RefreshMarkets();
                }

                if (AllUsers == null)
                {
                    RefreshUsers();
                }


                if (AllContexts == null)
                {
                    AllContexts = new Dictionary<int, Context>();
                }


                foreach (DataRow row in DBHelper.dtJobs.Rows)
                {
                    var job = new JobInfo(row);

                    if (job.IsLive)
                    {

                        

                        // grab the latest candles

                        var kline = CryptoGlobals.GetKlineFromString(job.Interval);
                        var c = CryptoGlobals.LastCandleInDB(AllMarkets[job.MarketId].Symbol, kline);

                        var shouldGetCandles = false;
                        var firstDate = DateTime.Today.AddDays(60);

                        if (c == null)
                        {
                            // ouch never got any candles :(
                            shouldGetCandles = true;
                        }
                        else
                        {
                            // we got, that's our start date

                            if (c.OpenTime < CryptoGlobals.MinusKlineInterval(DateTime.UtcNow, kline))
                            {
                                shouldGetCandles = true;
                                firstDate = c.OpenTime;
                            }
                        }

                        if (shouldGetCandles)
                        {
                            CandleHelper.RetrieveCandles(AllMarkets[job.MarketId].Symbol, kline, firstDate, DateTime.Now.AddDays(1)); // TAKE IT ALLLLL
                        }

                        if (!AllUsers.ContainsKey(job.UserId))
                        {
                            AllUsers = null;
                            RefreshUsers();
                            if (!AllUsers.ContainsKey(job.UserId))
                            {
                                // invalid user
                                continue;
                            }
                        }



                        var user = AllUsers[job.UserId];


                        // get candles

                        var crows = DBHelper.GetDataTable("SELECT * FROM Candles WHERE Symbol='" + AllMarkets[job.MarketId].Symbol + "' AND Interval='" + job.Interval + "'"); // haha crows



                        List<CandleInfo> CurrentCandles = new List<CandleInfo>();

                        foreach (DataRow crow in crows.Rows)
                        {
                            var tempC = new CandleInfo(crow);

                            CurrentCandles.Add(tempC);
                        }

                        StrategyOptions myStrat = null;
                        var strat = DBHelper.GetDataTable("SELECT * from Strategies where Id=" + job.ScriptId);
                        if (strat.Rows.Count > 0)
                        {
                            myStrat = (new StrategyInfo(strat.Rows[0])).Options;
                        }
                        else
                        {
                            continue; // cancel!?!?
                        }



                        Context cxt = null;
                        if (!AllContexts.ContainsKey(job.Id))
                        {
                            cxt = new Context(CurrentCandles);
                            cxt.IsLive = true;
                            cxt.User = user;
                            cxt.ActiveStrategyOptions = new StrategyOptions() { BacktestStartingBalance = 1000000, Fees = 0, MaxOpenOrders = myStrat.MaxOpenOrders, MinutesInBetweenOrders = myStrat.MinutesInBetweenOrders };
                        }
                        else
                        {
                            cxt = AllContexts[job.Id];
                            cxt.RawCandles = CurrentCandles;
                        }

                        

                        var l = new List<CandleInfo>();





                        if (CurrentCandles.Count == 0)
                        {
                            continue;
                        }


                        cxt.RawCandles = CurrentCandles;


                        if (cxt.IsLive && !cxt.IsPaper)
                        {
                            //var orders = CryptoGlobals.UserBinances[user.Username].GetOpenOrders();
                            //
                            //foreach (var item in orders.Data)
                            //{
                            //
                            //    if (item.Symbol != AllMarkets[job.MarketId].Symbol) continue;
                            //
                            //    var t = new OrderInfo(cxt, (float)(item.Price * item.ExecutedQuantity));
                            //    t.CoinAmount = (float)item.ExecutedQuantity;
                            //    t.IsOpen = true;
                            //    t.Id = item.ClientOrderId.ToString();
                            //}
                        }
                        else
                        {

                        }
                        

                        if (!Globals.RunScript(cxt.StrategyScript,ref cxt))
                        {
                            continue;
                        }





                        // we now can run the script

                    }
                }


                System.Diagnostics.Debug.WriteLine(DateTime.Now);
                await Task.Delay(1000);
                
            }
        }

        public void OnStopped()
        {
            System.Diagnostics.Debug.WriteLine("Closing");
        }
    }
}
