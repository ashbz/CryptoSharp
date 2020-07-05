using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoAPI.Classes;
using CryptoAPI.Classes.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Binance.Net.Objects;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using CryptoCore.Classes;

namespace CryptoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandlesController : ControllerBase
    {
        [HttpPost("{Symbol}")]
        [HttpGet("{Symbol}")]
        public ActionResult<string> Get(string Symbol)
        {
            var RequestData = new Dictionary<string, string>();

            RequestData = CryptoGlobals.GetRequestData(Request);

            Symbol = Symbol.Replace("-", "");

            var interval = KlineInterval.FifteenMinutes;
            var start = new DateTime(2019, 7, 1);
            var end = DateTime.Now;
            var symbol = Symbol.ToUpper();

            var liteJson = false;
            var justOK = false;
            var jsonP = "";

            

            if (RequestData.ContainsKey("interval")){
                var tempInterval = RequestData["interval"].ToString();

                interval = CryptoGlobals.GetKlineFromString(tempInterval);
            }
            else
            {
                return "interval missing";
            }

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if (RequestData.ContainsKey("jsonp"))
            {
                jsonP = RequestData["jsonp"];
            }

            if (RequestData.ContainsKey("justok"))
            {
                justOK = true;
            }

            if (RequestData.ContainsKey("lite") && RequestData["lite"] == "true")
            {
                liteJson = true;
            }

            if (RequestData.ContainsKey("start") && RequestData.ContainsKey("end"))
            {
                var startS = RequestData["start"].ToString();
                var endS = RequestData["end"].ToString();

                DateTime dt1;
                if (DateTime.TryParseExact(startS,
                                       "yyyy-MM-dd",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out dt1))
                {
                    start = dt1;
                }
                else
                {
                    return "Invalid start date";
                }

                DateTime dt2;
                if (DateTime.TryParseExact(endS,
                                       "yyyy-MM-dd",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out dt2))
                {
                    end = dt2.AddDays(1); // because we are NOT getting time. So we want to get UNTIL the next day
                    if (end > DateTime.Now)
                    {
                        end = DateTime.Now;
                    }
                }
                else
                {
                    return "Invalid end date";
                }
            }
            else
            {
                return JsonConvert.SerializeObject(new KeyValuePair<string,string>("Error","You have to use start & date query"));
            }


            if (!CandleHelper.Jobs.ContainsKey(symbol)|| (CandleHelper.Jobs.ContainsKey(symbol) && CandleHelper.Jobs[symbol].Done))
            {
                if (CandleHelper.Jobs.ContainsKey(symbol))
                {
                    CandleHelper.Jobs.Remove(symbol);
                }



                DateTime small = DateTime.MinValue;
                DateTime big = DateTime.MinValue;
                List<CandleInfo> allCandles = null;

                if (CandleHelper.CandlesAvailable(symbol, interval ,start, end,out small,out big,out allCandles))
                {
                    var finalS = "";

                    if (justOK)
                    {
                        finalS = "ok";
                    }
                    else
                    {
                        if (liteJson)
                        {
                            var temp = new List<CandleInfo>();
                            foreach (var item in allCandles)
                            {
                                temp.Add(item); // timestamp, o,h,l,c
                                                // highchart requires timestamp*1000 or else dates are invalid
                            }

                            finalS = JsonConvert.SerializeObject(temp);
                        }
                        else
                        {
                            finalS = JsonConvert.SerializeObject(allCandles);
                        }
                    }

                    

                    //Response.ContentType = "application/javascript";
                    if (jsonP != "")
                    {
                        return jsonP + "(" + finalS + ")";
                    }
                    return finalS;


                    // let's retrieve em all??
                    return "Small: " + small.ToString() + Environment.NewLine + "Big: " + big.ToString();
                }
                else
                {
                    if (big == DateTime.MinValue)
                    {
                        big = start;
                    }
                    else
                    {
                        big = big.MinusKlineInterval(interval).MinusKlineInterval(interval);
                    }
                    CandleHelper.Jobs[symbol] = new CandleJob(symbol, start, end, interval); // we just started the job
                    CandleHelper.Jobs[symbol].StartWork();

                    return "working";
                }

                
            }
            else
            {
                return "working";

            }

        }
    }
}