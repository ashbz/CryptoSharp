using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoAPI.Classes;
using CryptoAPI.Classes.Helpers;
using CryptoCore.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoAPI.Controllers
{
    [Produces("text/plain")]
    [Route("api/[controller]")]
    [ApiController]
    public class StrategiesController : ControllerBase
    {

        [HttpPost]
        [HttpGet]
        public ActionResult<string> All()
        {
            var RequestData = CryptoGlobals.GetRequestData(Request);
            
            if (!RequestData.ContainsKey("token") || !RequestData.ContainsKey("action"))
            {
                return "Token and/or action missing!";
            }

            var token = RequestData["token"].Trim();
            var action = RequestData["action"].Trim();



            UserInfo currUser = Globals.GetUserFromToken(token);

            if (currUser == null)
            {
                return "invalid token";
            }
            
            
            

            if (action == "get")
            {
                var sql = "SELECT * FROM Strategies WHERE UserId='" + currUser.Id + "'";

                var rows = DBHelper.GetDataTable(sql);

                var l = new List<StrategyInfo>();


                foreach (DataRow row in rows.Rows)
                {
                    var s = new StrategyInfo(row);

                    l.Add(s);
                }

                var res = JsonConvert.SerializeObject(l);

                return res;
                
            }else if (action == "new")
            {
                var script = @"using System.Linq;
using CryptoCore.Scripting;
using CryptoCore.Classes;


public class MyStrategy
{
    // how much to buy in USD
    float BUY_AMOUNT = 100f;
    
    // this event runs every time we have a new Candle (of the interval we have selected)
    public void OnData(Context context)
    {
        

    }   

}";
                var si = Globals.AddNewStrategyToDB(new StrategyInfo() { Name = "Strategy - New", UserId = currUser.Id, Script= script });
                return JsonConvert.SerializeObject(si);
            }
            else if (action == "update")
            {
                if (!RequestData.ContainsKey("strategy"))
                {
                    return "Invalid strategy";
                }

                StrategyInfo si = JsonConvert.DeserializeObject<StrategyInfo>(RequestData["strategy"]);
                
                if (si == null)
                {
                    return "Invalid strategy 2";
                }


                Globals.UpdateStrategy(si);

                return JsonConvert.SerializeObject(si);
            }else if (action == "delete")
            {
                if (!RequestData.ContainsKey("strategy"))
                {
                    return "Invalid strategy";
                }

                StrategyInfo si = JsonConvert.DeserializeObject<StrategyInfo>(RequestData["strategy"]);

                SQLiteCommand com = new SQLiteCommand();

                com.CommandText = "DELETE FROM Strategies WHERE Id=@Id";

                com.Parameters.AddWithValue("@Id", si.Id);

                DBHelper.ExecuteSqlCommand(com);

                return "ok";
            }
            else if (action == "rename")
            {
                if (!RequestData.ContainsKey("strategy"))
                {
                    return "Invalid strategy";
                }

                StrategyInfo si = JsonConvert.DeserializeObject<StrategyInfo>(RequestData["strategy"]);

                SQLiteCommand com = new SQLiteCommand();

                com.CommandText = "UPDATE Strategies SET Name=@Name WHERE Id=@Id";

                com.Parameters.AddWithValue("@Name", si.Name);
                com.Parameters.AddWithValue("@Id", si.Id);

                DBHelper.ExecuteSqlCommand(com);

                return "ok";
            }

            return "Error 33";
        }
    }
}