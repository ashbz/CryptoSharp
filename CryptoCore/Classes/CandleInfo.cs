using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class CandleInfo
    {
        public string Symbol;
        public float Open;
        public float Close;
        public float High;
        public float Low;
        public float Volume;

        public float MidPrice
        {
            get
            {
                return (this.Open + this.Close) / 2;
            }
        }

        [JsonConverter(typeof(DateFormatConverter), "MM/dd/yyyy HH:mm:ss")]
        public DateTime OpenTime;

        
        [JsonConstructor]
        public CandleInfo(string symbol, float open, float close, float high, float low, float volume, DateTime openTime)
        {
            Symbol = symbol;
            Open = float.Parse(open.ToString("n2"));
            Close = close;
            High = high;
            Low = low;
            Volume = volume;
            OpenTime = openTime;
        }

        public CandleInfo(DataRow row):this(row["Symbol"].ToString().ToUpper(),
                        float.Parse(row["Open"].ToString()),
                        float.Parse(row["Close"].ToString()),
                        float.Parse(row["High"].ToString()),
                        float.Parse(row["Low"].ToString()),
                        float.Parse(row["Volume"].ToString()), row["OpenTime"].ToString().ToSqlDateTime())
        {


        }


    }
}
