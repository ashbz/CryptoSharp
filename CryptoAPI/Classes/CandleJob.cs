using Binance.Net.Objects;
using CryptoAPI.Classes.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoAPI.Classes
{
    public class CandleJob
    {
        public string Symbol;
        public DateTime Start;
        public DateTime End;
        public KlineInterval Interval;
        public bool Working = false;
        public bool Done = false;
        private Thread _thread;

        public CandleJob(string symbol, DateTime start, DateTime end, KlineInterval interval)
        {
            Working = false;
            Done = false;
            Symbol = symbol;
            Start = start;
            End = end;
            Interval = interval;
        }

        public void StartWork()
        {
            if (Working) return;
            if (Done) return;

            _thread = new Thread(() => {

                Working = true;
                Done = false;
                CandleHelper.RetrieveCandles(Symbol, Interval, Start, End);
                Working = false;
                Done = true;
            });

            _thread.Start();
        }
        

    }
}
