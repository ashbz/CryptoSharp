using Binance.Net;
using Binance.Net.Objects;
using CryptoAPI.Classes.Helpers;
using CryptoCore.Classes;
using CSScriptLib;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CryptoAPI.Classes
{
    public static class CryptoGlobals
    {

        public static BinanceClient adminBinanceClient;

        public static List<string> GoodMarkets;

        public static BinanceAccountInfo adminAccountInfo;


        public static Dictionary<string, BinanceClient> UserBinances = new Dictionary<string, BinanceClient>(); 



        public static void InitGlobals()
        {
            var t = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            if (File.Exists(t + "/config.ini"))
            {

                // todo wat wat
                var ini = new CryptoCore.Classes.IniFile(t + "/config.ini");
                if (ini.KeyExists("binancy_key") && ini.KeyExists("binancy_secret"))
                {
                    Globals.DEFAULT_BINANCE_KEY = ini.Read("binance_key");
                    Globals.DEFAULT_BINANCE_SECRET = ini.Read("binance_secret");
                }
            }

            adminBinanceClient = new BinanceClient();
            adminBinanceClient.SetApiCredentials(CryptoCore.Classes.Globals.DEFAULT_BINANCE_KEY,
                CryptoCore.Classes.Globals.DEFAULT_BINANCE_SECRET); // just for candles

            var req = adminBinanceClient.GetAccountInfo();
            adminAccountInfo = req.Data;

            if (req.Error!=null)
            {
                // error
                adminAccountInfo = null;
            }

            var usdtBalance = GetBalance("usdt");
            var btc = GetBalance("btc");

            GoodMarkets = new List<string>();
            GoodMarkets.Add("BTCUSDT");
            GoodMarkets.Add("ETHUSDT");
            GoodMarkets.Add("BNBUSDT");
            GoodMarkets.Add("EOSUSDT");
            GoodMarkets.Add("BCHABCUSDT");
            GoodMarkets.Add("XRPUSDT");
            GoodMarkets.Add("LTCUSDT");
            GoodMarkets.Add("LINKUSDT");
            GoodMarkets.Add("USDCUSDT");
            GoodMarkets.Add("PAXUSDT");
            GoodMarkets.Add("TUSDUSDT");
            GoodMarkets.Add("TRXUSDT");
            GoodMarkets.Add("NEOUSDT");
            GoodMarkets.Add("ERDUSDT");
            GoodMarkets.Add("ADAUSDT");
            GoodMarkets.Add("BTTUSDT");
            GoodMarkets.Add("ETCUSDT");
            GoodMarkets.Add("ALGOUSDT");
            GoodMarkets.Add("WAVESUSDT");
            GoodMarkets.Add("ONTUSDT");
            GoodMarkets.Add("MATICUSDT");
            GoodMarkets.Add("ATOMUSDT");
            GoodMarkets.Add("XLMUSDT");
            GoodMarkets.Add("ONEUSDT");
            GoodMarkets.Add("ZECUSDT");
            GoodMarkets.Add("QTUMUSDT");
            GoodMarkets.Add("VETUSDT");
            GoodMarkets.Add("IOTAUSDT");
            GoodMarkets.Add("ICXUSDT");
            GoodMarkets.Add("XMRUSDT");
            GoodMarkets.Add("CELRUSDT");
            GoodMarkets.Add("ZILUSDT");
            GoodMarkets.Add("FETUSDT");
            GoodMarkets.Add("HOTUSDT");
            GoodMarkets.Add("DASHUSDT");
            GoodMarkets.Add("BATUSDT");
            GoodMarkets.Add("ENJUSDT");
            GoodMarkets.Add("DOGEUSDT");
            GoodMarkets.Add("FTMUSDT");
            GoodMarkets.Add("ZRXUSDT");
            GoodMarkets.Add("OMGUSDT");
            GoodMarkets.Add("THETAUSDT");
            GoodMarkets.Add("IOSTUSDT");
            GoodMarkets.Add("TFUELUSDT");
            GoodMarkets.Add("NANOUSDT");
            GoodMarkets.Add("NULSUSDT");
            GoodMarkets.Add("USDSUSDT");
            GoodMarkets.Add("MITHUSDT");
            GoodMarkets.Add("GTOUSDT");
            GoodMarkets.Add("ONGUSDT");
        }

        public static bool MarketExists(string Symbol)
        {
            return GoodMarkets.Contains(Symbol.ToUpper());
        }


        public static double GetBalance(string Market)
        {
            if (adminAccountInfo == null) return 0f;


            foreach (var item in adminAccountInfo.Balances)
            {
                if (item.Asset.ToLower() == Market.ToLower())
                {
                    return (double)item.Free;
                }
            }

            return 0f;
        }


        public static Dictionary<string,string> GetRequestData(HttpRequest Request)
        {
            Dictionary<string, string> RequestData = new Dictionary<string, string>();
            
            if (Request.Method == "GET")
            {
                foreach (var item in Request.Query)
                {
                    RequestData[item.Key.ToString()] = item.Value.ToString();
                }
            }
            else if (Request.Method == "POST")
            {
                StreamReader reader = new StreamReader(Request.Body);
                string s = reader.ReadToEnd();

                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(s);
                foreach (var kv in dict)
                {
                    //Console.WriteLine(kv.Key + ":" + kv.Value);
                    RequestData[kv.Key] = kv.Value;
                }

                //var sArray = s.Split('&');
                //foreach (var item in sArray)
                //{
                //    var tempParts = item.Split('=');
                //    if (tempParts.Length != 2)
                //    {
                //        continue;
                //    }
                //
                //    RequestData[tempParts[0]] = tempParts[1];
                //}
            }

            return RequestData;
        }
        public static void FillMarketsInDB(bool updatePrices = true)
        {
            bool shouldUpdate = false;

            var tempDt = DBHelper.GetDataTable("SELECT * FROM Markets");
            if (tempDt.Rows.Count > 0)
            {
                // we already have markets. But maybe update the prices.
                if (!updatePrices) return;

                shouldUpdate = true;
            }

            var markets24 = CryptoGlobals.adminBinanceClient.Get24HPricesList();

            if (markets24 != null && markets24.Data!=null && markets24.Data.Count() > 0)
            {
                // all good
            }
            else
            {
                return;
            }
            var finalMarkets = new List<Binance24HPrice>();

            foreach (var item in markets24.Data)
            {
                if (CryptoGlobals.GoodMarkets.Contains(item.Symbol))
                {
                    finalMarkets.Add(item);
                }
            }


            List<SQLiteCommand> list = new List<SQLiteCommand>();

            foreach (var item in finalMarkets)
            {
                SQLiteCommand com = new SQLiteCommand();

                if (!shouldUpdate)
                {
                    com.CommandText = "INSERT OR IGNORE INTO Markets " +
                    "(Symbol,Enabled,Volume,FirstCandleTime,LastPrice) " +
                    "VALUES (@Symbol,@Enabled,@Volume,@FirstCandleTime,@LastPrice)";

                    
                }
                else
                {
                    com.CommandText = "UPDATE Markets SET LastPrice=@LastPrice WHERE Symbol=@Symbol";
                }

                com.Parameters.AddWithValue("@Volume", item.QuoteVolume);
                com.Parameters.AddWithValue("@FirstCandleTime", CandleHelper.GetFirstCandleDate(item.Symbol).ToSqlDateString());
                com.Parameters.AddWithValue("@Enabled", 1);

                com.Parameters.AddWithValue("@Symbol", item.Symbol);
                com.Parameters.AddWithValue("@LastPrice", item.LastPrice);

                list.Add(com);
            }

            DBHelper.BulkExecuteNonQuery(list);

        }

        public static CandleInfo LastCandleInDB(string market, KlineInterval interval)
        {
            var dt = DBHelper.GetDataTable("SELECT * FROM Candles WHERE Symbol='" + market + "' and Interval=" + interval.ToString() + " order by Id desc LIMIT 1");

            if (dt.Rows.Count > 0)
            {
                return new CandleInfo(dt.Rows[dt.Rows.Count - 1]);
            }
            else
            {
                return null;
            }
        }

        public static DateTime MinusKlineInterval(this DateTime date, KlineInterval interval)
        {
            var testDate = date;
            switch (interval)
            {
                case KlineInterval.OneMinute:
                    testDate = date.AddMinutes(-1);
                    break;
                case KlineInterval.ThreeMinutes:
                    testDate = date.AddMinutes(-3);
                    break;
                case KlineInterval.FiveMinutes:
                    testDate = date.AddMinutes(-5);
                    break;
                case KlineInterval.FifteenMinutes:
                    testDate = date.AddMinutes(-15);
                    break;
                case KlineInterval.ThirtyMinutes:
                    testDate = date.AddMinutes(-30);
                    break;
                case KlineInterval.OneHour:
                    testDate = date.AddMinutes(-60);
                    break;
                case KlineInterval.TwoHour:
                    testDate = date.AddMinutes(-120);
                    break;
                case KlineInterval.FourHour:
                    testDate = date.AddHours(-4);
                    break;
                case KlineInterval.SixHour:
                    testDate = date.AddHours(-6);
                    break;
                case KlineInterval.EightHour:
                    testDate = date.AddHours(-8);
                    break;
                case KlineInterval.TwelveHour:
                    testDate = date.AddHours(-12);
                    break;
                case KlineInterval.OneDay:
                    testDate = date.AddHours(-24);
                    break;
                case KlineInterval.ThreeDay:
                    testDate = date.AddDays(-3);
                    break;
                case KlineInterval.OneWeek:
                    testDate = date.AddDays(-7);
                    break;
                case KlineInterval.OneMonth:
                    testDate = date.AddMonths(-1);
                    break;
                default:
                    break;
            }

            return testDate;
        }


       
        public static KlineInterval GetKlineFromString(string s)
        {
            KlineInterval interval = KlineInterval.FifteenMinutes;

            switch (s)
            {
                case "OneMinute":
                    interval = KlineInterval.OneMinute;
                    break;
                case "FiveMinutes":
                    interval = KlineInterval.FiveMinutes;
                    break;
                case "FifteenMinutes":
                    interval = KlineInterval.FifteenMinutes;
                    break;
                case "ThirtyMinutes":
                    interval = KlineInterval.ThirtyMinutes;
                    break;
                case "OneHour":
                    interval = KlineInterval.OneHour;
                    break;
                case "TwoHour":
                    interval = KlineInterval.TwoHour;
                    break;
                case "FourHour":
                    interval = KlineInterval.FourHour;
                    break;
                case "OneDay":
                    interval = KlineInterval.OneDay;
                    break;
                case "OneWeek":
                    interval = KlineInterval.OneWeek;
                    break;
                default:
                    interval = KlineInterval.FifteenMinutes;
                    break;
            }
            /*if (s == "FiveMinutes")
            {
                return KlineInterval.FiveMinutes;
            }else if (s == "FifteenMinutes")
            {
                return KlineInterval.FifteenMinutes;
            }
            else if (s == "ThirtyMinutes")
            {
                return KlineInterval.ThirtyMinutes;
            }
            else if (s == "OneHour")
            {
                return KlineInterval.OneHour;
            }
            else if (s == "TwoHour")
            {
                return KlineInterval.TwoHour;
            }
            else if (s == "FourHour")
            {
                return KlineInterval.FourHour;
            }
            else if (s == "OneDay")
            {
                return KlineInterval.OneDay;
            }
            */

            return interval;


            //return KlineFromString("FiveMinutes");
        }




        public static bool RunScript(string source, ref Context c)
        {
            
            try
            {
                CSScript.EvaluatorConfig.DebugBuild = false;
                //dynamic script = CSScript.RoslynEvaluator.
                dynamic script = CSScript.RoslynEvaluator.LoadCode(source);

                script.OnData(c);

                return true;
            }
            catch (Exception ex)
            {
                var st = ex.StackTrace;
                c.SCRIPT_ERROR = ex.Message;

                //MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + st);
                return false;
            }
        }

    }
}
