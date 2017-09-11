using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.WPFModules.Models
{
    /// <summary>
    /// 交易列表
    /// </summary>
    public class TradeBookModel
    {
        /// <summary>
        /// 交易Id
        /// </summary>
        public long TradeId { get; set; }
        /// <summary>
        /// 买单Id
        /// </summary>
        public string BuyOrderId { get; set; }
        /// <summary>
        /// 买单委托量
        /// </summary>
        public decimal BuyVolume { get; set; }
        /// <summary>
        /// 买单委托价
        /// </summary>
        public decimal BuyPrice { get; set; }
        /// <summary>
        /// 买单成交量（也是卖单委托量）
        /// </summary>
        public decimal BuyTradeVolume { get; set; }
        /// <summary>
        /// 买单成交均价
        /// </summary>
        public decimal BuyTradePrice { get; set; }
        /// <summary>
        /// 买单成交总额
        /// </summary>
        public decimal BuyTradeAmount { get; set; }
        /// <summary>
        /// 卖单Id
        /// </summary>
        public string SellOrderId { get; set; }
        /// <summary>
        /// 卖单委托量
        /// </summary>
        public decimal SellVolume { get; set; }
        /// <summary>
        /// 卖单委托价
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// 卖单成交量
        /// </summary>
        public decimal SellTradeVolume { get; set; }
        /// <summary>
        /// 卖单成交均价
        /// </summary>
        public decimal SellTradePrice { get; set; }
        /// <summary>
        /// 卖单成交总额
        /// </summary>
        public decimal SellTradeAmount { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 交易利润
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// 创建委托时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新委托时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
