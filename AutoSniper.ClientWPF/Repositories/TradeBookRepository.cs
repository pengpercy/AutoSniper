using AutoSniper.ClientWPF.Repositories.Models;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Repositories
{
    public static class TradeBookRepository
    {
        /// <summary>
        /// 创建TradeBook
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int CreateTradeBook(TradeBook model)
        {
            var sql = $@"INSERT INTO TradeBook(TradeId,BuyOrderId,BuyPrice,BuyVolume,BuyCompletedVolume,BuyAmount,SellOrderId,SellPrice,SellVolumn,SellCompletedVolume,SellAmount,Profit,Status,UpdateDate,CreateDate) 
                       VALUES(NULL,'{model.BuyOrderId}',{model.BuyPrice},{model.BuyVolume},{model.BuyCompletedVolume},{model.BuyAmount},'{model.SellOrderId}',{model.SellPrice},{model.SellVolume},{model.SellCompletedVolume},{model.SellAmount},{model.Profit},'{model.Status}','{model.UpdateDate}','{model.CreateDate}')";
            return DataProvider.GetConnection().Execute(sql);
        }

        public static int CreateTradeBook2(TradeBook model)
        {
            return DataProvider.GetConnection().Insert(model);
        }
    }
}
