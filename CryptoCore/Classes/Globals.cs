using CSScriptLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoCore.Classes
{
    public static class Globals
    {

        public static readonly string DATE_FORMAT = "MM/dd/yyyy HH:mm:ss";

        public static string API_ENDPOINT = "http://localhost:5000/";
        //public static string API_ENDPOINT = "https://localhost:5001/";

        public static string DEFAULT_BINANCE_KEY = "";
        public static string DEFAULT_BINANCE_SECRET = "";


        public static UserInfo CurrentUser;
        public static StrategyOptions ActiveStrategyOptions = new StrategyOptions();

        public enum BarValue
        {
            Low,
            High,
            Open,
            Close
        }


        public static string IND_SimpleMovingAverage = "Simple Moving Average";
        public static string IND_BollingerBands = "Bollinger Bands";
        public static string IND_MACD = "MACD";
        public static string IND_RSI = "RSI";
        public static string IND_ExponentialMovingAverage = "Exponential Moving Average";
        public static string IND_ParabolicSAR = "Parabolic SAR";
        public static string IND_CCI = "CCI";

        

        public static string IND_ATR = "ATR"; // let's ignore
        public static string IND_SD = "Standard Deviation";
        public static string IND_ADX = "ADX";
        public static string IND_WilliamsR = "Williams %R";


        public static List<string> GetTechnicalIndicatorList()
        {
            var list = new List<string>() {
                IND_SimpleMovingAverage,
                IND_ExponentialMovingAverage,
                IND_BollingerBands,
                IND_RSI,
                IND_ParabolicSAR,
                IND_CCI,
                IND_SD,
                IND_ADX,
                IND_WilliamsR

            };

            return list;
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }


        public static UserInfo LoginUser(string username, string password)
        {
            foreach (DataRow item in DBHelper.dtUsers.Rows)
            {
                var t = new UserInfo(item);

                if (t.Username == username && t.Password == password)
                {
                    return t;
                }

            }

            return null;
        }

        public static UserInfo GetUserFromToken(string token)
        {
            UserInfo currUser = null;

            foreach (DataRow item in DBHelper.dtUsers.Rows)
            {
                var u = new UserInfo(item);

                if (u.GetToken() == token)
                {
                    currUser = u;
                    break;
                }
            }

            return currUser;
        }

        public static bool SignupUser(string username, string password)
        {
            // check if all ok

            foreach (DataRow row in DBHelper.dtUsers.Rows)
            {
                var ui = new UserInfo(row);
                if (ui.Username == username)
                {
                    return false;
                }
            }



            SQLiteCommand sql = new SQLiteCommand();

            sql.CommandText = "INSERT INTO Users (Username,Password,Email,Enabled,DateCreated,BinanceKey,BinanceSecret) VALUES(@Username,@Password,@Email,@Enabled,@DateCreated,@BinanceKey,@BinanceSecret)";

            sql.Parameters.AddWithValue("@Username", username);
            sql.Parameters.AddWithValue("@Password", password);
            sql.Parameters.AddWithValue("@Email", "");
            sql.Parameters.AddWithValue("@Enabled", 1);
            sql.Parameters.AddWithValue("@DateCreated", DateTime.Today.ToSqlDateString());

            sql.Parameters.AddWithValue("@BinanceKey", "");

            sql.Parameters.AddWithValue("@BinanceSecret", "");



            DBHelper.ExecuteSqlCommand(sql);

            return true;
        }


        public static void UpdateUser(UserInfo user)
        {
            SQLiteCommand sql = new SQLiteCommand();

            sql.CommandText = "UPDATE Users SET Email=@Email,Password=@Password,BinanceKey=@BinanceKey,BinanceSecret=@BinanceSecret WHERE Id=@Id";

            sql.Parameters.AddWithValue("@Email", user.Email);
            sql.Parameters.AddWithValue("@Password", user.Password);
            sql.Parameters.AddWithValue("@BinanceKey", user.BinanceKey);
            sql.Parameters.AddWithValue("@BinanceSecret", user.BinanceSecret);

            sql.Parameters.AddWithValue("@Id",user.Id);
        }


        public static string HTTPGet(string url)
        {
            var http = new RestClient(url);
            http.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);

            var req = new RestRequest("", Method.GET);
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            var data = http.Get(req);
            var html = data.Content;

            return html;
        }


        public static string HTTPPost(string url,Dictionary<string,string> data)
        {
            var http = new RestClient(url);
            http.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);

            var req = new RestRequest("", Method.POST);
            req.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            req.AddJsonBody(JsonConvert.SerializeObject(data));
            req.Timeout = 999999999;
            var res = http.Post(req);
            var html = res.Content;
            

            return html;
        }



        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static float GetPercentage(float current, float max)
        {
            var tmp = (current / max) * 100f;
            return tmp;
        }
        

        public static StrategyInfo AddNewStrategyToDB(StrategyInfo si)
        {
            SQLiteCommand sql = new SQLiteCommand();
            sql.CommandText = "INSERT INTO Strategies (Name,UserId,Script,Description,DateCreated,DateModified,Options) VALUES(@Name,@UserId,@Script,@Description,@DateCreated,@DateModified,@Options)";

            sql.Parameters.AddWithValue("@Name", si.Name);
            sql.Parameters.AddWithValue("@UserId", si.UserId);
            sql.Parameters.AddWithValue("@Script", si.Script);
            sql.Parameters.AddWithValue("@Description", si.Description);
            sql.Parameters.AddWithValue("@Options", JsonConvert.SerializeObject(new StrategyOptions()));
            sql.Parameters.AddWithValue("@DateCreated", DateTime.Now.ToSqlDateString());
            sql.Parameters.AddWithValue("@DateModified", DateTime.Now.ToSqlDateString());

            DBHelper.ExecuteSqlCommand(sql);

            // let's get the id!
            var rows = DBHelper.dtStrategies.Rows;
            var lastId = Convert.ToInt32(rows[rows.Count - 1]["Id"]);
            si.Id = lastId;

            return si;

        }



        public static void UpdateStrategy(StrategyInfo si)
        {
            SQLiteCommand sql = new SQLiteCommand();
            sql.CommandText = "UPDATE Strategies SET Name=@Name,Script=@Script,Options=@Options,Description=@Description,DateModified=@DateModified WHERE Id=@Id";

            sql.Parameters.AddWithValue("@Name", si.Name);
            sql.Parameters.AddWithValue("@Script", si.Script);
            sql.Parameters.AddWithValue("@Description", si.Description);
            sql.Parameters.AddWithValue("@Options", JsonConvert.SerializeObject(si.Options));
            sql.Parameters.AddWithValue("@DateModified", si.DateModified.ToSqlDateString());
            sql.Parameters.AddWithValue("@Id", si.Id);

            DBHelper.ExecuteSqlCommand(sql);

        }



        public static DataRow GetLastDTRow(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return dt.AsEnumerable().Last();
        }


        public static DateTime ToSqlDateTime(this string s)
        {
            var altFormat = "MM-dd-yyyy HH:mm:ss";

            if (!s.Contains("-"))
            {
                altFormat = Globals.DATE_FORMAT;
            }

            try
            {
                return DateTime.ParseExact(s, altFormat, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                var res = DateTime.ParseExact(s, altFormat, CultureInfo.InvariantCulture);
                return res;
            }
            //return DateTime.Parse(s); 


        }

        public static long ToTimestamp(this DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }

        public static string ToSqlDateString(this DateTime dt)
        {
            return dt.ToString(Globals.DATE_FORMAT);
        }



        


        public static bool RunScript(string source, ref Context c, bool throwMessageBox = true)
        {
            //CSScript.GlobalSettings.InMemoryAssembly = false;

            try
            {
                //dynamic script = CSScript.LoadCode(source)
                //          .CreateObject("*");

                dynamic wat = CSScript.RoslynEvaluator.LoadCode(source);

                wat.OnData(c);

                return true;
            }
            catch (Exception ex)
            {
                var st = ex.StackTrace;

                if (throwMessageBox)
                {

                    MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + st);

                }


                return false;
            }
        }


        public static string TestScript(string source, ref Context c)
        {
            try
            {

                dynamic wat = CSScript.RoslynEvaluator.LoadCode(source);

                wat.OnData(c);

                return "";
            }
            catch (Exception ex)
            {
                var st = ex.StackTrace;

                return ex.Message;
            }
        }





    }
}
