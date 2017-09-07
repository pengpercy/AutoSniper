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
        public int TradeId { get; set; }
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
        /// 买单完成量
        /// </summary>
        public decimal BuyCompletedVolume { get; set; }
        /// <summary>
        /// 买单交易额
        /// </summary>
        public decimal BuyAmount { get; set; }
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
        /// 卖单完成量
        /// </summary>
        public decimal SellCompletedVolume { get; set; }
        /// <summary>
        /// 卖单交易额
        /// </summary>
        public decimal SellAmount { get; set; }
        /// <summary>
        /// 利润单价
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public TradeStatus Status { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }

    /// <summary>
    /// 交易状态
    /// </summary>
    public enum TradeStatus
    {
        /// <summary>
        /// 买单中
        /// </summary>
        Buying = 0,
        /// <summary>
        /// 部分完成卖单
        /// </summary>
        PartiallyBuy = 1,
        /// <summary>
        /// 卖单中
        /// </summary>
        Selling = 2,
        /// <summary>
        /// 部分完成买单
        /// </summary>
        PartiallySell = 3,
        /// <summary>
        /// 全部卖完
        /// </summary>
        AllCompleted = 4,
        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 5
    }
}
