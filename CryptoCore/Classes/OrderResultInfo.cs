using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class OrderResultInfo
    {

        public string Id;
        public DateTime Time;
        public string Type;
        public double OrderPrice;
        public double OrderAmountUSD;
        public double ProfitLoss;
        public double FinalBalance;
        public double FinalEquity;

        public OrderResultInfo()
        {

        }
    }
}
