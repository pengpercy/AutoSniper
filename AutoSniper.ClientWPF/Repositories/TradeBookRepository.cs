using AutoSniper.ClientWPF.Repositories.BaseProvider;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.Framework.Converter;
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
        /// 查询交易订单,默认UpdateDate降序
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="sortColumn">排序列名：UpdateDate，CreateDate</param>
        /// <param name="sortType">升降序: ASC，DESC</param>
        /// <returns></returns>
        public static IEnumerable<TradeBook> GetActiveOrder(string sortColumn = "", string sortType = "")
        {
            var sort = string.IsNullOrWhiteSpace(sortColumn) ? "UpdateDate DESC" : $" {sortColumn} {sortType}";
            var sql = $@"SELECT TradeId,BuyOrderId,BuyPrice,BuyVolume,BuyTradeVolume,BuyTradePrice,BuyAmount,SellOrderId,SellPrice,SellVolume,SellTradeVolume,SellTradePrice,SellAmount,Profit,Status,UpdateDate,CreateDate FROM TradeBook WHERE Status NOT IN ('{TradeStatus.已取消}','{TradeStatus.已完成}') ORDER BY {sort}";
            return DataProvider.GetConnection().Query<TradeBook>(sql);
        }

        /// <summary>
        /// 创建TradeBook
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int BuyOrder(TradeBook model)
        {
            var sql = $@"INSERT INTO TradeBook(TradeId,BuyOrderId,BuyPrice,BuyVolume,BuyTradeVolume,BuyTradePrice,BuyAmount,SellOrderId,SellPrice,SellVolume,SellTradeVolume,SellTradePrice,SellAmount,Profit,Status,UpdateDate,CreateDate) 
                       VALUES(NULL,'{model.BuyOrderId}',{model.BuyPrice},{model.BuyVolume},{model.BuyTradeVolume},{model.BuyTradePrice},{model.BuyAmount},'{model.SellOrderId}',{model.SellPrice},{model.SellVolume},{model.SellTradeVolume},{model.SellTradePrice},{model.SellAmount},{model.Profit},'{model.Status}','{model.UpdateDate}','{model.CreateDate}')";
            return DataProvider.GetConnection().Execute(sql);
        }

        /// <summary>
        /// 自动卖单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int SellOrder(TradeBook model)
        {
            var sql = $@"UPDATE TradeBook SET BuyTradeVolume={model.BuyTradeVolume}, BuyAmount={model.BuyAmount},SellOrderId='{model.SellOrderId}',SellPrice={model.SellPrice},SellVolume={model.SellVolume},SellTradeVolume={model.SellTradeVolume},SellAmount={model.SellAmount},Status='{model.Status}',UpdateDate={DateTime.Now} WHERE TradeId={model.TradeId}";
            return DataProvider.GetConnection().Execute(sql);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="tradeId"></param>
        /// <returns></returns>
        public static int CancelOrder(int tradeId)
        {
            var sql = $"UPDATE TradeBook SET Status='{TradeStatus.已取消}' WHERE TradeId={tradeId}";
            return DataProvider.GetConnection().Execute(sql);
        }
    }
}
