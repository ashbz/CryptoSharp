using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class MarketInfo
    {
        public int Id;
        public string Symbol;
        public bool Enabled;
        public float Volume;
        public float LastPrice;
        public DateTime FirstCandleTime;

        public MarketInfo()
        {
            
        }
        public MarketInfo(DataRow row)
        {
            Id = int.Parse(row["Id"].ToString()); // we do not cast to int, because SQLite int = int64 ...
            Symbol = (string)row["Symbol"].ToString().Replace("USDT", "-USDT");
            Volume = (float)(double)row["Volume"];
            Enabled = (int.Parse(row["Enabled"].ToString())==1) ? true : false;
            LastPrice = (float)(double)row["LastPrice"];
        }

        public MarketInfo(int id, string symbol, bool enabled, float volume, float lastPrice, DateTime firstCandleTime){

            Id = id;
            Symbol = symbol;
            Enabled = enabled;
            Volume = volume;
            LastPrice = lastPrice;
            FirstCandleTime = firstCandleTime;

        }

    }
}
