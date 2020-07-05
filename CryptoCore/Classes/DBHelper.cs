using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CryptoCore.Classes;

namespace CryptoCore.Classes
{
    public static class DBHelper
    {
        private static SQLiteConnection _connection;
        public static string LastError = "";
        public static string ConnectionString = @"Data Source=db.sqlite;Pooling=true;FailIfMissing=false;Version=3";
        public static string HackConnectionString = "";

        private static DataTable _dtMarkets;
        private static DataTable _dtCandles;
        private static DataTable _dtUsers;
        private static DataTable _dtStrategies;
        private static DataTable _dtJobs;
        private static DataTable _dtJobOrders;

        public static DataTable dtMarkets
        {
            get
            {
                _dtMarkets = DBHelper.GetDataTable("SELECT * FROM Markets");
                _dtMarkets.TableName = "Markets";

                return _dtMarkets;
            }
            set
            {
                _dtMarkets = value;
            }
        }

        public static DataTable dtCandles
        {
            get
            {
                _dtCandles = DBHelper.GetDataTable("SELECT * FROM Candles");
                _dtCandles.TableName = "Candles";

                return _dtCandles;
            }
            set
            {
                _dtCandles = value;
            }
        }

        public static DataTable dtUsers
        {
            get
            {
                _dtUsers = DBHelper.GetDataTable("SELECT * FROM Users");
                _dtUsers.TableName = "Users";

                return _dtUsers;
            }
            set
            {
                _dtUsers = value;
            }
        }

        public static DataTable dtStrategies
        {
            get
            {
                _dtStrategies = DBHelper.GetDataTable("SELECT * FROM Strategies"); // WHERE UserId=" + Globals.CurrentUser.Id
                _dtStrategies.TableName = "Strategies";

                return _dtStrategies;
            }
            set
            {
                _dtStrategies = value;
            }
        }


        public static DataTable dtJobs
        {
            get
            {
                _dtJobs = DBHelper.GetDataTable("SELECT * FROM Jobs");
                _dtJobs.TableName = "Jobs";

                return _dtJobs;
            }
            set
            {
                _dtJobs = value;
            }
        }

        public static DataTable dtJobOrders
        {
            get
            {
                _dtJobOrders = DBHelper.GetDataTable("SELECT * FROM JobOrders");
                _dtJobOrders.TableName = "JobOrders";

                return _dtJobOrders;
            }
            set
            {
                _dtJobOrders = value;
            }
        }


        public static DataTable GetDataTable(string selectCommand)
        {
            BeginDB();

            var sql = new SQLiteDataAdapter(selectCommand, _connection);

            var t = new DataTable();
            try
            {
                sql.Fill(t);
            }
            catch (Exception e)
            {
                // ????
                LastError = e.Message;
            }

            EndDB();

            return t;
        }

        public static void ExecuteSqlCommand(SQLiteCommand command)
        {
            BeginDB();

            command.Connection = _connection;
            try
            {
                

                var read = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
            finally
            {
                EndDB();
            }
        }


        public static void BeginDB() //int timeout = 15
        {
            LastError = "";

            

            if (_connection == null)
            {
                if (HackConnectionString != "")
                {
                    ConnectionString = @"Data Source='D:\Ash\Ptixiaki\dotnet project\CryptoAPI\CryptoAPI\bin\Debug\netcoreapp2.1\db.sqlite';Pooling=true;FailIfMissing=false;Version=3";
                }

                _connection = new SQLiteConnection(ConnectionString);
            }

            if (_connection.State == ConnectionState.Closed)
            {
                try
                {
                    _connection.Open();
                    if (_connection.State != ConnectionState.Open)
                    {
                        LastError = "Something wrong papa";

                    }
                }
                catch (Exception e)
                {
                    LastError = e.Message;
                }
            }
        }


        public static void BulkExecuteNonQuery(List<SQLiteCommand> commands)
        {
            Debug.WriteLine("START_BULK");
            try
            {
                BeginDB();
                SQLiteTransaction transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    foreach (SQLiteCommand com in commands)
                    {
                        com.Connection = _connection;
                        var temp = com.ExecuteNonQuery();
                        com.Dispose();
                    }

                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
            }
            finally
            {
                EndDB();
            }
            Debug.WriteLine("END_BULK");

        }



        public static void EndDB()
        {
            return; // let's not close the connection


            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }


        private static bool TableExists(string tableName)
        {
            bool tableExists = false;

            BeginDB();

            DataRow[] rows = _connection.GetSchema("Tables").Select(string.Format("Table_Name = '{0}'", tableName));
            tableExists = (rows.Length > 0);
            

            return tableExists;
        }

        public static void CreateTables()
        {
            var tblMarkets = "Markets";
            var tblCandles = "Candles";
            var tblUsers = "Users";
            var tblStrategies = "Strategies";
            var tblJobs = "Jobs";
            var tblJobOrders = "JobOrders";


            if (!TableExists(tblMarkets))
            {
                // create table
                SQLiteCommand sql = new SQLiteCommand();

                var tbl = new QTable(tblMarkets);
                tbl.Columns.Add(new QTableColumn("Id", "INTEGER", false, true, true));
                tbl.Columns.Add(new QTableColumn("Symbol", "TEXT", false, false, false, true));
                tbl.Columns.Add(new QTableColumn("Enabled", "INT", true, false, false));
                tbl.Columns.Add(new QTableColumn("Volume", "REAL", true, false, false));
                tbl.Columns.Add(new QTableColumn("LastPrice", "REAL", true, false, false));
                tbl.Columns.Add(new QTableColumn("FirstCandleTime", "TEXT", true, false, false));


                sql.CommandText = tbl.GenerateCreateSQL();

                ExecuteSqlCommand(sql);


                if (!TableExists(tblMarkets))
                {
                    // cancel everything?
                    return;
                }
                else
                {
                    
                }
            }

            if (!TableExists(tblCandles))
            {
                // create table
                SQLiteCommand sql = new SQLiteCommand();

                var tbl = new QTable(tblCandles);
                tbl.Columns.Add(new QTableColumn("Id", "INTEGER", false, true, true));
                tbl.Columns.Add(new QTableColumn("Symbol", "TEXT", false, false, false,true));
                tbl.Columns.Add(new QTableColumn("Interval", "TEXT", false, false, false, true));
                tbl.Columns.Add(new QTableColumn("Volume", "REAL", false, false, false));
                tbl.Columns.Add(new QTableColumn("Close", "REAL", false, false, false));
                tbl.Columns.Add(new QTableColumn("High", "REAL", false, false, false));
                tbl.Columns.Add(new QTableColumn("Low", "REAL", false, false, false));
                tbl.Columns.Add(new QTableColumn("Open", "REAL", false, false, false));
                tbl.Columns.Add(new QTableColumn("OpenTime", "TEXT", false, false, false,true));

                sql.CommandText = tbl.GenerateCreateSQL();

                ExecuteSqlCommand(sql);


                if (!TableExists(tblCandles))
                {
                    return;
                }
            }

            if (!TableExists(tblUsers))
            {
                SQLiteCommand sql = new SQLiteCommand();

                var tbl = new QTable(tblUsers);

                tbl.Columns.Add(new QTableColumn("Id", "INTEGER", false, true, true));
                tbl.Columns.Add(new QTableColumn("Username", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("Password", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("Email", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("Enabled", "INT", false, false, false));
                tbl.Columns.Add(new QTableColumn("DateCreated", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("BinanceKey", "TEXT", true, false, false));
                tbl.Columns.Add(new QTableColumn("BinanceSecret", "TEXT", true, false, false));

                sql.CommandText = tbl.GenerateCreateSQL();

                ExecuteSqlCommand(sql);

                if (!TableExists(tblUsers))
                {
                    return;
                }

                // let's add default user

                sql.CommandText = "INSERT INTO Users (Username,Password,Email,Enabled,DateCreated,BinanceKey,BinanceSecret) VALUES(@Username,@Password,@Email,@Enabled,@DateCreated,@BinanceKey,@BinanceSecret)";

                sql.Parameters.AddWithValue("@Username", "admin");
                sql.Parameters.AddWithValue("@Password", "123");
                sql.Parameters.AddWithValue("@Email", "cse43584@uniwa.gr");
                sql.Parameters.AddWithValue("@Enabled", 1);
                sql.Parameters.AddWithValue("@DateCreated", DateTime.Today.ToSqlDateString());
                sql.Parameters.AddWithValue("@BinanceKey", Globals.DEFAULT_BINANCE_KEY); // todo remove these binance keys
                sql.Parameters.AddWithValue("@BinanceSecret", Globals.DEFAULT_BINANCE_SECRET);


                ExecuteSqlCommand(sql);

            }

            if (!TableExists(tblStrategies))
            {
                SQLiteCommand sql = new SQLiteCommand();

                var tbl = new QTable(tblStrategies);

                tbl.Columns.Add(new QTableColumn("Id", "INTEGER", false, true, true));
                tbl.Columns.Add(new QTableColumn("Name", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("Description", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("UserId", "INT", false, false, false));
                tbl.Columns.Add(new QTableColumn("Script", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("Options", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("DateCreated", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("DateModified", "TEXT", false, false, false));

                sql.CommandText = tbl.GenerateCreateSQL();

                ExecuteSqlCommand(sql);

                if (!TableExists(tblStrategies))
                {
                    return;
                }
            }

            if (!TableExists(tblJobs))
            {
                SQLiteCommand sql = new SQLiteCommand();

                var tbl = new QTable(tblJobs);

                tbl.Columns.Add(new QTableColumn("Id", "INTEGER", false, true, true));
                tbl.Columns.Add(new QTableColumn("Name", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("Description", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("UserId", "INT", false, false, false));
                tbl.Columns.Add(new QTableColumn("ScriptId", "INT", false, false, false));
                tbl.Columns.Add(new QTableColumn("IsLive", "INT", false, false, false));
                tbl.Columns.Add(new QTableColumn("Interval", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("MarketId", "INT", false, false, false));
                tbl.Columns.Add(new QTableColumn("DateCreated", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("LastCandleDate", "TEXT", false, false, false));
                tbl.Columns.Add(new QTableColumn("IsEnabled", "INT", false, false, false));

                sql.CommandText = tbl.GenerateCreateSQL();

                ExecuteSqlCommand(sql);

                if (!TableExists(tblJobs))
                {
                    return;
                }
            }



            if (!TableExists(tblJobOrders))
            {
                SQLiteCommand sql = new SQLiteCommand();

                var tbl = new QTable(tblJobOrders);

                tbl.Columns.Add(new QTableColumn("Id", "INTEGER", false, true, true));
                tbl.Columns.Add(new QTableColumn("UserId", "INT", false, false, false));
                tbl.Columns.Add(new QTableColumn("ScriptId", "INT", false, false, false));
                tbl.Columns.Add(new QTableColumn("BinanceOrderId", "TEXT", false, false, false));

                sql.CommandText = tbl.GenerateCreateSQL();

                ExecuteSqlCommand(sql);

                if (!TableExists(tblJobOrders))
                {
                    return;
                }
            }

        }

    }
}
