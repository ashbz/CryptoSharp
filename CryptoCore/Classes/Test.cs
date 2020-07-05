using CryptoCore.Classes;
using CryptoCore.Scripting;
using System.Linq;

    public class Test
    {

        public void OnStart(Context context)
        {

        }

        public void OnData(Context context)
        {
            var sma14 = Indicators.SimpleMovingAverage(14, context.CloseValues);

            var buy1 = sma14.Last() < context.LastCandle.Low;
            var buy2 = sma14.Last() < context.RawCandles[context.RawCandles.Count - 2].Low;
            var sell1 = sma14.Last() > context.LastCandle.High;

            


            if (buy1)
            {
                if (context.GetOpenOrders().Count == 0)
                {
                    var ord = context.Buy(5f);


                    Helper.Log("Bought @ " + ord.BuyPrice);
                }
            }

            if (sell1 && context.GetOpenOrders().Count > 0)
            {
                // we have 1 active buy order, we should sell it
                if ((context.LastCandle.OpenTime - context.GetOpenOrders()[0].BuyCandle.OpenTime).TotalMinutes > 240)
                {
                    // we want to sell only after at least 30 mins have passed
                    context.CloseOrder(context.GetOpenOrders()[0]);
                }
            }

        }   

        public void OnEnd(Context context)
        {

        }

    }

