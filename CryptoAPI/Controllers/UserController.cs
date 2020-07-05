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
    public class UserController : ControllerBase
    {

        [HttpPost]
        [HttpGet]
        public ActionResult<string> All()
        {
            var RequestData = CryptoGlobals.GetRequestData(Request);

            if (RequestData.ContainsKey("action"))
            {
                var action = RequestData["action"];
                
                if (action == "login")
                {
                    var user = RequestData["username"];
                    var pass = RequestData["password"];

                    var tempUI = Globals.LoginUser(user, pass);

                    if (tempUI == null)
                    {
                        return "error";
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(tempUI);
                    }
                }else if (action == "signup")
                {
                    var user = RequestData["username"];
                    var pass = RequestData["password"];
                    
                    if (Globals.SignupUser(user, pass)){
                        // all good
                        return "success";
                    }
                    else
                    {
                        return "error";
                    }
                }

                // we need token
                if (!RequestData.ContainsKey("token"))
                {
                    return "invalid token";
                }

                var token = RequestData["token"];

                var ui = Globals.GetUserFromToken(token);

                if (ui == null)
                {
                    return "invalid token";
                }

            }

            return "";
        }

       
    }
}