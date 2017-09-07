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
            if (Connection != null)
                return Connection;
            var dbFilePath = "./AutoSniper.db";
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
                #region Create table Sql string
                string sqlCreateTable = @"
create table Currency (
   CurrencyId           integer  PRIMARY KEY AUTOINCREMENT,
   Name                 nvarchar(16)         not null,
   TakerRate            decimal              not null,
   MakerRate            decimal              null,
   DefaultProfit        decimal              null,
   UpdateDate           datetime             not null ,
   CreateDate           datetime             not null DEFAULT (datetime('now','localtime'))
);

create table TradeBook (
   TradeId              integer  PRIMARY KEY AUTOINCREMENT,
   BuyOrderId           nvarchar(64)               not null,
   BuyPrice             decimal              not null,
   BuyVolume            decimal              not null,
   BuyCompletedVolume   decimal              not null,
   BuyAmount            decimal              not null,
   SellOrderId          nvarchar(64)               not null,
   SellPrice            decimal              not null,
   SellVolumn           decimal              not null,
   SellCompletedVolume  decimal              not null,
   SellAmount           decimal              not null,
   Profit               decimal              not null,
   Status               nvarchar(64)               not null,
   UpdateDate           datetime             not null ,
   CreateDate           datetime             not null DEFAULT (datetime('now','localtime'))
)";
                #endregion

                Connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", dbFilePath));
                Connection.Open();
                Connection.Execute(sqlCreateTable);
            }
            Connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", dbFilePath));
            Connection.Open();
            return Connection;
        }
    }
}
