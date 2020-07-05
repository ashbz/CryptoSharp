using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public class UserInfo
    {
        public int Id;
        public string Username;
        public string Password;
        public string Email;
        public bool Enabled;
        public DateTime DateCreated;

        public string BinanceKey;
        public string BinanceSecret;

        public UserInfo()
        {
        }


        public UserInfo(DataRow row)
        {
            Id = Convert.ToInt32(row["Id"]);
            Username = (string)row["Username"];
            Password = (string)row["Password"];
            Email = (string)row["Email"];
            Enabled = (row["Username"].ToString()=="1") ? true : false;
            DateCreated = row["DateCreated"].ToString().ToSqlDateTime();

            BinanceKey = row["BinanceKey"].ToString();
            BinanceSecret = row["BinanceSecret"].ToString();

        }

        public string GetToken()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Username + ":" + this.Password));
        }

    }
}
