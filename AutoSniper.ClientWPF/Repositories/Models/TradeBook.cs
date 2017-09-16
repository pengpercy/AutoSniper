using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Repositories.Models
{
    /// <summary>
    /// 交易记录
    /// </summary>
    public class TradeBook
    {
        /// <summary>
        /// 交易Id
        /// </summary>
        public long? TradeId { get; set; }
        /// <summary>
        /// 买单Id
        /// </summary>
        public string BuyOrderId { get; set; }
        /// <summary>
        /// 买单价格
        /// </summary>
        public decimal BuyPrice { get; set; }
        /// <summary>
        /// 买单委托量
        /// </summary>
        public decimal BuyVolume { get; set; }
        /// <summary>
        /// 买单成交均价
        /// </summary>
        public decimal BuyTradePrice { get; set; }
        /// <summary>
        /// 买单完成量
        /// </summary>
        public decimal BuyTradeVolume { get; set; }
        /// <summary>
        /// 买单交易额
        /// </summary>
        public decimal BuyTradeAmount { get; set; }
        /// <summary>
        /// 卖单Id
        /// </summary>
        public string SellOrderId { get; set; }
        /// <summary>
        /// 卖单价
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// 卖单委托量
        /// </summary>
        public decimal SellVolume { get; set; }
        /// <summary>
        /// 卖单成交均价
        /// </summary>
        public decimal SellTradePrice { get; set; }
        /// <summary>
        /// 卖单完成量
        /// </summary>
        public decimal SellTradeVolume { get; set; }
        /// <summary>
        /// 卖单交易额
        /// </summary>
        public decimal SellTradeAmount { get; set; }
        /// <summary>
        /// 利润单价
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }
    }

    /// <summary>
    /// 交易状态
    /// </summary>
    public enum TradeStatus
    {
        /// <summary>
        /// 买单中
        /// </summary>
        买单中 = 0,
        /// <summary>
        /// 部分买单
        /// </summary>
        部分买单 = 1,
        /// <summary>
        /// 卖单中
        /// </summary>
        卖单中 = 2,
        /// <summary>
        /// 部分卖单
        /// </summary>
        部分卖单 = 3,
        /// <summary>
        /// 已完成
        /// </summary>
        已完成 = 4,
        /// <summary>
        /// 已取消
        /// </summary>
        已取消 = 5
    }
}
