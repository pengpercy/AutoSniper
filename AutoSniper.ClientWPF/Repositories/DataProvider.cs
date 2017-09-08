using AutoSniper.ClientWPF.Repositories.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Repositories
{
    public class DataProvider
    {
        private static SQLiteConnection Connection;
        public static SQLiteConnection GetConnection()
        {
            if (Connection != null) { return Connection; }
            var dbFilePath = "./AutoSniper.db";
            var connectionString = string.Format("Data Source={0};Version=3;", dbFilePath);
            if (File.Exists(dbFilePath))
            {
                Connection = new SQLiteConnection(connectionString);
                Connection.Open();
                return Connection;
            }

            SQLiteConnection.CreateFile(dbFilePath);
            Connection = new SQLiteConnection(connectionString);
            Connection.Open();
            Connection.Execute(File.ReadAllText("../../AppData/Database.sql"));
            return Connection;
        }
    }
}
