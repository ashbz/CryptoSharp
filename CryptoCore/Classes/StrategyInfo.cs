using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class StrategyInfo
    {

        public int Id = -1;
        public string Name = "";
        public string Description = "";
        public int UserId = -1;
        public string Script = "";
        public DateTime DateCreated = DateTime.MinValue;
        public DateTime DateModified = DateTime.MinValue;
        public StrategyOptions Options;


        public StrategyInfo()
        {
            Id = -1;
            Name = "";
            Description = "";
            UserId = -1;
            Script = "";
            DateCreated = DateTime.MinValue;
            DateModified = DateTime.MinValue;
            Options = new StrategyOptions();
        }

        public StrategyInfo(DataRow row)
        {
            Id = Convert.ToInt32(row["Id"]);
            Name = (string)row["Name"];
            UserId = Convert.ToInt32(row["UserId"]);
            Script = (string)row["Script"];
            Description = (string)row["Description"];
            DateCreated = row["DateCreated"].ToString().ToSqlDateTime();
            DateModified = row["DateModified"].ToString().ToSqlDateTime();
            if (row["Options"] != DBNull.Value && row["Options"].ToString()!="null")
            {
                Options = JsonConvert.DeserializeObject<StrategyOptions>(row["Options"].ToString());
            }
            else
            {
                Options = new StrategyOptions();
            }
        }

    }
}
