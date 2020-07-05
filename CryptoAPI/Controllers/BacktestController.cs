using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Binance.Net.Objects;
using CryptoAPI.Classes;
using CryptoAPI.Classes.Helpers;
using CryptoCore.Classes;
using CSScriptLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacktestController : ControllerBase
    {

        [HttpPost]
        [HttpGet]
        public ActionResult<string> All()
        {
            var RequestData = CryptoGlobals.GetRequestData(Request);

            if (RequestData.ContainsKey("action") && RequestData.ContainsKey("source") && RequestData.ContainsKey("options") && RequestData.ContainsKey("token"))
            {
                var action = RequestData["action"];
                var token = RequestData["token"];
                var source = RequestData["source"];
                var market = RequestData["market"];
                var startS = RequestData["start"];
                var endS = RequestData["end"];
                var interval = RequestData["interval"];
                var options = RequestData["options"];

                var ui = Globals.GetUserFromToken(token);

                if (ui == null)
                {
                    return "invalid token";
                }

                if (action == "backtest")
                {
                    var tOptions = JsonConvert.DeserializeObject<StrategyOptions>(options);

                    if (tOptions == null)
                    {
                        return "invalid strategy options";
                    }

                    var cInterval = CryptoGlobals.GetKlineFromString(interval);

                    DateTime start = DateTime.MinValue;
                    DateTime end = DateTime.MaxValue;

                    DateTime dt1,dt2;
                    if (DateTime.TryParseExact(startS,
                                           "yyyy-MM-dd",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out dt1))
                    {
                        start = dt1;
                    }
                    else
                    {
                        return "Invalid start date";
                    }


                    if (DateTime.TryParseExact(endS,
                                           "yyyy-MM-dd",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out dt2))
                    {
                        end = dt2;
                    }
                    else
                    {
                        return "Invalid end date";
                    }


                    var res = Backtest(source, market, cInterval, start, end, tOptions);

                    return res;

                }



            }

            return "";
        }



        public string Backtest(string source, string market, KlineInterval kline, DateTime start, DateTime end, StrategyOptions actStrat)
        {
            //var source = "";
            //var market = "BTCTUSDT";
            //var kline = Binance.Net.Objects.KlineInterval.FifteenMinutes;
            //var start = new DateTime(2019, 11, 1);
            //var end = new DateTime(2019, 12, 30);
            //var actStrat = new StrategyOptions();

            // get all candles
            CandleHelper.RetrieveCandles(market,kline,start,end);


            DateTime small = DateTime.MinValue;
            DateTime big = DateTime.MinValue;
            List<CandleInfo> allCandles = null;


            if (!CandleHelper.CandlesAvailable(market,kline,start,end,out small, out big, out allCandles))
            {
                return "no candles available";
            }



            var temp = new Context(allCandles);


            temp.ActiveStrategyOptions = Globals.ActiveStrategyOptions;
            if (!CryptoGlobals.RunScript(source, ref temp))
            {
                return "error in script: " + temp.SCRIPT_ERROR;
            }



            var c = new Context(allCandles);

            c.ActiveStrategyOptions = actStrat;

            var startingBalance = c.ActiveStrategyOptions.BacktestStartingBalance;

            var originalBalance = startingBalance;

            var l = new List<CandleInfo>();

            dynamic script = CSScript.RoslynEvaluator.LoadCode(source);

            foreach (var item in allCandles)
            {
                l.Add(item);

                if (l.Count < 20) continue;

                c.RawCandles = l;

                try
                {
                    script.OnData(c);

                }catch(Exception ex)
                {
                    return "error: " + ex.Message;
                }
            }


            foreach (var item in c.GetOpenOrders())
            {
                c.CloseOrder(item);
            }



            var orders = c.GetAllOrders();

            var candlesAndOrders = new SortedDictionary<DateTime, List<OrderInfo>>();

            foreach (var item in orders)
            {
                var buyDate = item.BuyCandle.OpenTime;
                var sellDate = item.SellCandle.OpenTime;

                if (!candlesAndOrders.ContainsKey(buyDate))
                {
                    candlesAndOrders[buyDate] = new List<OrderInfo>();
                }

                if (!candlesAndOrders.ContainsKey(sellDate))
                {
                    candlesAndOrders[sellDate] = new List<OrderInfo>();
                }


                candlesAndOrders[buyDate].Add(item);
                candlesAndOrders[sellDate].Add(item);

            }

            candlesAndOrders.OrderBy(x => x.Key);




            var originalEquity = originalBalance;


            double finalEquity = 0f;

            var equityOrders = new Dictionary<string, OrderInfo>();

            List<OrderResultInfo> finalOrders = new List<OrderResultInfo>();

            double totalProfits = 0f;


            var tempCount = 0;
            var alreadyAddedIds = new List<string>();
            foreach (var cao in candlesAndOrders)
            {
                var currDate = cao.Key;
                var orderList = cao.Value;
                foreach (var item in orderList)
                {

                    tempCount++;

                    var isSell = false;
                    if (alreadyAddedIds.Contains(item.Id))
                    {
                        isSell = true;
                    }
                    else
                    {
                        alreadyAddedIds.Add(item.Id);
                    }

                    var ord = item;

                    var id = ord.Id;
                    string type;
                    DateTime time;
                    double orderPrice = 0.0f; ;
                    double size = ord.BuyAmountInUSD;
                    double profitLoss = 0f;
                    double balance = 0f;






                    if (isSell == false) // buy
                    {
                        originalBalance -= ord.BuyAmountInUSD;

                        originalBalance = Math.Round(originalBalance, 2);

                        time = ord.BuyCandle.OpenTime;
                        type = "Buy";
                        orderPrice = Math.Round(ord.BuyPrice, 2);

                        equityOrders[ord.Id] = ord;
                    }
                    else // sell
                    {
                        var tUsd = ord.TotalUSD();
                        tUsd = Math.Round(tUsd, 2);

                        originalBalance += tUsd;

                        originalBalance = Math.Round(originalBalance, 2);

                        time = ord.SellCandle.OpenTime;
                        type = "Sell";
                        var tmpPL = Math.Round(tUsd - ord.BuyAmountInUSD, 2);
                        profitLoss = tmpPL;
                        balance = originalBalance;
                        balance = Math.Round(balance, 2);


                        totalProfits += tmpPL;

                        orderPrice = Math.Round(ord.GetSellPrice(), 2);

                        equityOrders.Remove(ord.Id);
                    }


                    size = Math.Round(size, 8);


                    var finalBalance = balance;// (isSell) ? "$ " + balance.ToString() : "";

                    finalEquity = 0f;


                    CandleInfo tempCandle = (isSell) ? orderList[0].SellCandle : orderList[0].BuyCandle;

                    double ttttemp = 0f;
                    foreach (var eOrd in equityOrders.Values)
                    {
                        finalEquity += tempCandle.MidPrice * ord.CoinAmount;
                        ttttemp += ord.BuyAmountInUSD;
                    }

                    ttttemp = Math.Round(ttttemp, 3);

                    finalEquity = Math.Round(finalEquity, 3);

                    var ori = new OrderResultInfo(){ Id = id.ToString(), Time = time, Type = type, OrderPrice = orderPrice, OrderAmountUSD = size, ProfitLoss = profitLoss, FinalBalance = finalBalance, FinalEquity = finalEquity};
                    finalOrders.Add(ori);
                }



            }

            var finalPL = Math.Round(originalBalance - startingBalance, 2);
            finalOrders.Add(new OrderResultInfo() { Id = "", Time = DateTime.MinValue, Type = "blank", OrderPrice = 0f, OrderAmountUSD = 0f, ProfitLoss = 0f, FinalBalance = 0f });
            finalOrders.Add(new OrderResultInfo() { Id = "", Time = DateTime.MinValue, Type = "blank2", OrderPrice = 0f, OrderAmountUSD = 0f, ProfitLoss = finalPL, FinalBalance = originalBalance});

            var final = JsonConvert.SerializeObject(finalOrders);

            return final;
            

        }


    }
}