using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class StrategyOptions
    {
        public double BacktestStartingBalance = 1000; // in USD

        public int MinutesInBetweenOrders = 30;
        public int MaxOpenOrders = 5;
        public double Fees = 0.25f; // out of 100 percent

    }
}
