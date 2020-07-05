using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
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
    public class MarketsController : ControllerBase
    {

        [HttpPost]
        [HttpGet]
        public ActionResult<string> All()
        {
            var RequestData = CryptoGlobals.GetRequestData(Request);
            var jsonP = "";

            if (RequestData.ContainsKey("jsonp"))
            {
                jsonP = RequestData["jsonp"];
            }

            if (RequestData.ContainsKey("market"))
            {
                var Symbol = RequestData["market"].Replace("-", "");
                var sql = "SELECT * FROM Markets WHERE Symbol='" + Symbol.ToUpper() + "'";

                var rows = DBHelper.GetDataTable(sql);


                if (rows.Rows.Count != 1)
                {
                    return "{}";
                }
                else
                {
                    var res = JsonConvert.SerializeObject(rows.Rows[0].Table);
                    //res = res.Substring(1, res.Length - 2); // we do not want the [] in the beginning and end
                    return res;
                }
            }
            else
            {
                if (RequestData.ContainsKey("refresh"))
                {
                    CryptoGlobals.FillMarketsInDB(true);
                }


                var rows = DBHelper.dtMarkets.Rows;

                var markets = new List<MarketInfo>();

                foreach (DataRow row in rows)
                {
                    markets.Add(new MarketInfo(row));
                }

                var res = JsonConvert.SerializeObject(markets);

                //Response.ContentType = "application/javascript";


                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                Response.Headers.Add("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,OPTIONS");

                if (jsonP != "")
                {
                    return jsonP + "(" + res + ")";
                }
                return res;
            }

            
        }
    }
}