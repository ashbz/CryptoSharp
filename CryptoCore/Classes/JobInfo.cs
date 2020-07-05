using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class JobInfo
    {

        public int Id;
        public string Name;
        public string Description;
        public int UserId;
        public int ScriptId;
        public int MarketId;
        public bool IsLive;
        public bool IsEnabled;
        public string Interval;
        public DateTime DateCreated;
        public DateTime LastData;


        public JobInfo(DataRow row)
        {
            Id = Convert.ToInt32(row["Id"]);
            Name = (string)row["Name"];
            Name = (string)row["Description"];
            UserId = Convert.ToInt32(row["UserId"]);
            ScriptId = Convert.ToInt32(row["ScriptId"]);
            MarketId = Convert.ToInt32(row["MarketId"]);
            IsLive = (Convert.ToInt32(row["IsLive"])==1) ? true : false;
            IsEnabled = (Convert.ToInt32(row["IsEnabled"])==1) ? true : false;
            DateCreated = ((string)row["DateCreated"]).ToSqlDateTime();
            LastData = ((string)row["LastData"]).ToSqlDateTime();

        }



    }
}
