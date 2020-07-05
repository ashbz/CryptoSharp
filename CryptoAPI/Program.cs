using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net;
using CryptoAPI.Classes;
using CryptoAPI.Classes.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CryptoCore;
using CryptoCore.Classes;

namespace CryptoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var c = new Context(new List<CandleInfo>());
            //var w = CryptoGlobals.RunScript("Console.WriteLine(2.ToString());", ref c);


            CryptoGlobals.InitGlobals();


            var balances = CryptoGlobals.adminBinanceClient.GetAccountInfo();

            DBHelper.CreateTables();
            CryptoGlobals.FillMarketsInDB();

            var test = CryptoGlobals.adminBinanceClient.Get24HPricesList();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).
                UseStartup<Startup>();

        }
    }
}
