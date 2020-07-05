using Binance.Net.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using CryptoCore.Classes;

namespace CryptoAPI.Classes.Helpers
{
    public static class CandleHelper
    {
        public static Dictionary<string, CandleJob> Jobs = new Dictionary<string, CandleJob>();

        public static void RetrieveCandles(string symbolName, KlineInterval interval, DateTime startDate, DateTime endDate)
        {
            startDate = startDate.AddDays(-1);
            endDate = endDate.AddDays(1);
            startDate = startDate.ToUniversalTime();
            endDate = endDate.ToUniversalTime();

            //endDate = DateTime.Today.AddDays(1); // ALWAYS get until the end. Hack but good hack

            DateTime smallestDateAvailable = DateTime.MinValue;
            DateTime biggestDateAvailable = DateTime.MinValue;
            List<CandleInfo> allAvailableCandles = null;
            if (CandlesAvailable(symbolName, interval, startDate, endDate, out smallestDateAvailable, out biggestDateAvailable, out allAvailableCandles))
            {
                // we have all the candles we needdadad
                return;
            }

            var tempFirstDate = CandleHelper.GetFirstCandleDate(symbolName.ToUpper());

            // if wanted start date is < than the first available candle in Binance
            if (startDate < tempFirstDate)
            {
                startDate = tempFirstDate;
            }



            bool isDone = false;

            Dictionary<DateTime, BinanceKline> candles = new Dictionary<DateTime, BinanceKline>();

            DateTime latestOpenDate = DateTime.MinValue;

            var newStartDate = startDate;
            var newEndDate = endDate;


            while (!isDone)
            {

                var tempCandles = CryptoGlobals.adminBinanceClient.GetKlines(symbolName, interval, newStartDate, newEndDate, 1000);

                foreach (var c in tempCandles.Data)
                {
                    var cOpenTime = c.OpenTime;

                    if (!candles.ContainsKey(cOpenTime))
                    {
                        candles[cOpenTime] = c;


                        if (cOpenTime > latestOpenDate)
                        {
                            latestOpenDate = cOpenTime;
                        }
                    }
                }

                var testDate = DateTime.MinValue;

                testDate = newEndDate.MinusKlineInterval(interval);

                // we are asking for 1000 MAX candles. 
                // If the result is not 1000, means we have gotten them all.
                if (tempCandles.Data.Count() != 1000 || latestOpenDate >= testDate)
                {
                    isDone = true;
                }
                else
                {
                    if (latestOpenDate != DateTime.MinValue)
                    {
                        newStartDate = latestOpenDate;
                    }
                    isDone = false;
                }
            }




            var list = new List<SQLiteCommand>();


            foreach (var temp in candles)
            {
                var candle = temp.Value;

                SQLiteCommand com = new SQLiteCommand();
                com.CommandText = "INSERT OR IGNORE INTO Candles (Symbol,Interval,Volume,Close,High,Low,Open,OpenTime) " +
                    "VALUES (@Symbol,@Interval,@Volume,@Close,@High,@Low,@Open,@OpenTime)";

                com.Parameters.AddWithValue("@Symbol", symbolName);
                com.Parameters.AddWithValue("@Interval", interval.ToString());
                com.Parameters.AddWithValue("@Volume", candle.Volume);
                com.Parameters.AddWithValue("@Close", candle.Close);
                com.Parameters.AddWithValue("@High", candle.High);
                com.Parameters.AddWithValue("@Low", candle.Low);
                com.Parameters.AddWithValue("@Open", candle.Open);
                com.Parameters.AddWithValue("@OpenTime", candle.OpenTime.ToSqlDateString());

                list.Add(com);
            }

            DBHelper.BulkExecuteNonQuery(list);
        }

        private static Dictionary<string, DateTime> MarketStartDates;
        public static DateTime GetFirstCandleDate(string symbolName)
        {
            if (MarketStartDates == null)
            {
                MarketStartDates = new Dictionary<string, DateTime>();
            }

            if (MarketStartDates.ContainsKey(symbolName.ToUpper()))
            {
                return MarketStartDates[symbolName.ToUpper()];
            }
            else
            {
                // try to get it from the DB
                var dt = DBHelper.GetDataTable("SELECT * FROM Markets WHERE Symbol='" + symbolName.ToUpper() + "' LIMIT 1");

                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["FirstCandleTime"] == DBNull.Value)
                    {
                        // we don't have a valid date, let's get it
                    }
                    else{
                        MarketStartDates[symbolName.ToUpper()] = dt.Rows[0]["FirstCandleTime"].ToString().ToSqlDateTime();
                        return GetFirstCandleDate(symbolName);
                    }
                }


                // we have to get it and update the DB
                var klines = CryptoGlobals.adminBinanceClient.GetKlines(symbolName, KlineInterval.OneMinute, new DateTime(2010, 1, 1),DateTime.Today,5);

                if (klines.Data.Count() > 0)
                {

                    // update DB
                    var com = new SQLiteCommand();
                    com.CommandText = "UPDATE Markets SET FirstCandleTime=@FirstCandleTime WHERE Symbol=@Symbol";

                    com.Parameters.AddWithValue("@FirstCandleTime", klines.Data[0].OpenTime.ToSqlDateString());
                    com.Parameters.AddWithValue("@Symbol", symbolName);

                    DBHelper.ExecuteSqlCommand(com);

                    MarketStartDates[symbolName.ToUpper()] = klines.Data[0].OpenTime;

                    return CandleHelper.GetFirstCandleDate(symbolName.ToUpper());
                }
                else
                {
                    // there was an issue
                }

                return DateTime.MinValue;
            }
            


        }





        public static bool CandlesAvailable(string symbolName, KlineInterval interval, DateTime wantedStartDate, DateTime wantedEndDate, out DateTime SmallestDateAvailable, out DateTime BiggestDateAvailable, out List<CandleInfo> FoundCandles)
        {
            
            if (wantedEndDate.Date == DateTime.Today.Date)
            {
                //endDate = DateTime.Today.Date.AddDays(1);
            }


            wantedEndDate = wantedEndDate.ToUniversalTime();
            wantedStartDate = wantedStartDate.ToUniversalTime();

            var currentDate = DateTime.Now.ToUniversalTime();

            var firstDateAVailableInDB = GetFirstCandleDate(symbolName);
            if (wantedStartDate < firstDateAVailableInDB)
            {
                wantedStartDate = firstDateAVailableInDB;
            }



            var dt = DBHelper.GetDataTable("SELECT * FROM Candles WHERE Symbol='" + symbolName.ToUpper() + "' AND Interval='" + interval.ToString() + "'  order by OpenTime ASC");

            var smallestAvailableDate = DateTime.MaxValue;
            var biggestAvailableDate = DateTime.MinValue;

            FoundCandles = new List<CandleInfo>();

            var openDates = new List<DateTime>();

            foreach (DataRow row in dt.Rows)
            {
                var tempDate = row["OpenTime"].ToString().ToSqlDateTime();
                if (tempDate <= wantedEndDate && tempDate>=wantedStartDate)
                {
                    openDates.Add(tempDate);

                    FoundCandles.Add(new CandleInfo(symbolName.ToUpper(),
                        float.Parse(row["Open"].ToString()),
                        float.Parse(row["Close"].ToString()),
                        float.Parse(row["High"].ToString()),
                        float.Parse(row["Low"].ToString()),
                        float.Parse(row["Volume"].ToString()), tempDate));

                }

                if (tempDate > biggestAvailableDate) biggestAvailableDate = tempDate;
                if (tempDate < smallestAvailableDate) smallestAvailableDate = tempDate;
            }


            FoundCandles = FoundCandles.OrderBy(o => o.OpenTime).ToList();

            if (dt.Rows.Count == 0)
            {
                SmallestDateAvailable = DateTime.MinValue;
                BiggestDateAvailable = DateTime.MinValue;
            }
            else
            {
                SmallestDateAvailable = smallestAvailableDate;
                BiggestDateAvailable = biggestAvailableDate;
            }

            bool smallOK = openDates.Count!=0 && smallestAvailableDate <= wantedStartDate; // true
            bool bigOK = BiggestDateAvailable >= wantedEndDate.MinusKlineInterval(interval);
            bool noGaps = true;

            openDates.Sort();

            var BINANCE_UPGRADE_DAYS = new List<DateTime>() { new DateTime(2019, 11, 13), new DateTime(2019, 11, 25) };

            var lastOpen = DateTime.MinValue;
            if (openDates.Count > 2)
            {
                lastOpen = openDates[0];
            

                for (int i=1;i<openDates.Count;i++)
                {
                    var item = openDates[i];

                    if (item.MinusKlineInterval(interval) != lastOpen && (item - lastOpen).TotalDays>1)
                    {
                         // more than 1 day of gap = GAP  
                         // otherwise we have some binance upgrades, which mean we will NOT get any candles for short spans of time
                         // BOOYA
                        noGaps = false;
                        break;
                        
                        
                    }
                    else
                    {
                        lastOpen = item;

                    }

                }

            }

            if (smallOK && bigOK && openDates.Count!=0 && noGaps==true) // && openDates.First().Date == startDate.Date&& openDates.Last().Date >= endDate.MinusKlineInterval(interval)
            {
                return true;
            }
            else
            {
                return false;
            }

            return smallOK && bigOK && openDates.Count != 0;
        }

    }
}
