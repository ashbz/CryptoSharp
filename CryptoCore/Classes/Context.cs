using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CryptoCore.Classes.Globals;

namespace CryptoCore.Classes
{
    public class Context
    {
        public List<CandleInfo> RawCandles;

        public string SCRIPT_ERROR = "";
        public CandleInfo LastCandle
        {
            get
            {
                return RawCandles.Last();
            }
        }

        public StrategyOptions ActiveStrategyOptions;

        public int MaxOrderCount;

        public DateTime LastBuyTime = DateTime.MinValue;
        public DateTime LastSellTime = DateTime.MinValue;

        public UserInfo User = null;
        public MarketInfo Market = null;
        public string StrategyScript = "";

        public bool IsLive = false;
        public bool IsPaper = true;


        public double[] OpenValues
        {
            get
            {
                return GetValuesAsArray(BarValue.Open);
            }
        }
        public double[] HighValues
        {
            get
            {
                return GetValuesAsArray(BarValue.High);
            }
        }
        public double[] LowValues
        {
            get
            {
                return GetValuesAsArray(BarValue.Low);
            }
        }
        public double[] CloseValues
        {
            get
            {
                return GetValuesAsArray(BarValue.Close);
            }
        }

        public List<OrderInfo> Orders;


        public Context(List<CandleInfo> candles)
        {
            RawCandles = candles;

            Orders = new List<OrderInfo>();
        }

        public List<OrderInfo> GetOpenOrders()
        {
            return Orders.Where(ord => ord.IsOpen).ToList();
        }

        public List<OrderInfo> GetAllOrders()
        {
            return Orders;
        }

        public OrderInfo Buy(double USDTamount)
        {
            if (GetOpenOrders().Count >= ActiveStrategyOptions.MaxOpenOrders) return null;

            if ((LastCandle.OpenTime - LastBuyTime).TotalMinutes < ActiveStrategyOptions.MinutesInBetweenOrders) return null;

            LastBuyTime = LastCandle.OpenTime;

            var oi = new OrderInfo(this, USDTamount*(1- ActiveStrategyOptions.Fees/100)); // 5 USDT

            if (IsLive && User!=null)
            {
                if (IsPaper)
                {
                    Orders.Add(oi);
                }
                else
                {
                    // var rc = Globals.HTTPGet("https://localhost:5001/api/user/?action=buy&token=" + this.User.GetToken() + "&market=" + this.Market.Symbol + "&quantity=" + amountInUSDT);
                    // oi.Id = rc;
                }
            }
            else
            {
                // paper trading
            }

            Orders.Add(oi);


            return oi;
        }

        public void CloseOrder(OrderInfo order)
        {
            

            if (Orders.Contains(order))
            {

    
                LastSellTime = LastCandle.OpenTime;

                if (IsLive)
                {
                    if (IsPaper)
                    {
                        order.IsOpen = false;
                        order.SellCandle = LastCandle;

                    }
                    else
                    {
                        // we should sell here :(
                        //var rc = Globals.HTTPGet("https://localhost:5001/api/user/?action=sell&token=" + User.GetToken() + "&market=" + Market.Symbol + "&orderid=" + order.Id);
                        //
                        //if (rc == "sold")
                        //{
                        //    // all gucci!
                        //}
                    }
                }
                else
                {
                    order.IsOpen = false;
                    order.SellCandle = LastCandle;
                }
  
            }
        }




        private double[] GetValuesAsArray(BarValue barValue)
        {
            var l = new double[RawCandles.Count];
            
            for (int i = 0; i < RawCandles.Count; i++)
            {

                double temp = 0f;

                switch (barValue)
                {
                    case BarValue.Low:
                        temp = RawCandles[i].Low;
                        break;
                    case BarValue.High:
                        temp = RawCandles[i].High;
                        break;
                    case BarValue.Open:
                        temp = RawCandles[i].Open;
                        break;
                    case BarValue.Close:
                        temp = RawCandles[i].Close;
                        break;
                }


                l[i] = temp;
            }

            return l;
        }


        public double GetEquityNOT_USED(CandleInfo candle)
        {
            var allOrders = GetAllOrders();

            double totalEquity = 0f;

            foreach (var ord in allOrders)
            {
                var alreadyClosed = false;
                if (ord.SellCandle!=null && ord.SellCandle.OpenTime <= candle.OpenTime)
                {
                    alreadyClosed = true;
                }

                if (ord.BuyCandle.OpenTime > candle.OpenTime)
                {
                    break;
                }


                if (alreadyClosed)
                {
                    // we have closed, we calculate using the sellcandle
                    totalEquity += ord.CoinAmount * ord.SellCandle.MidPrice;
                }
                else
                {
                    totalEquity += ord.CoinAmount * candle.MidPrice;
                }
            }


            return totalEquity;

        }


    }
}
