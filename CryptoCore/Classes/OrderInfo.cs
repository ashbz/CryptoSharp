using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class OrderInfo
    {
        public string Id;

        public double BuyAmountInUSD;

        public double CoinAmount;

        public double BuyPrice;

        public CandleInfo BuyCandle;
        public CandleInfo SellCandle;

        public bool IsOpen = false;

        public Context ParentContext;

        public OrderInfo(Context _context, double howMuchUSD)
        {

            if (_context.User == null)
            {
                _context.IsLive = false;
                _context.IsPaper = true;
            }

            if (_context.IsLive && _context.IsPaper == false)
            {
                // binance ID
            }
            else
            {
                Id = CryptoCore.Classes.Globals.RandomNumber(10000000, 99999999).ToString();
            }


            IsOpen = true;
            ParentContext = _context;
            BuyAmountInUSD = howMuchUSD;
            BuyCandle = _context.LastCandle;
            BuyPrice = BuyCandle.MidPrice;

            CoinAmount = Globals.GetPercentage((float)BuyAmountInUSD, (float)BuyPrice) / 100f;


        }


        public double TotalUSD()
        {
            var tmp = (CoinAmount * SellCandle.MidPrice)*(1-ParentContext.ActiveStrategyOptions.Fees/100);
            return tmp;
        }

        public double GetSellPrice()
        {
            if (SellCandle == null)
            {
                return 0.0f;
            }

            return SellCandle.MidPrice;
        }


        public double CurrentProfit()
        {
            var tmp = (ParentContext.LastCandle.MidPrice * CoinAmount) - BuyAmountInUSD;
            tmp = (1 - ParentContext.ActiveStrategyOptions.Fees / 100) * tmp;
            return tmp;
        }
    }
}
