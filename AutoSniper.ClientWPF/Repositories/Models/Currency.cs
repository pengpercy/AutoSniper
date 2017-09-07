using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Repositories.Models
{
    /// <summary>
    /// 货币相关信息
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// 货币Id
        /// </summary>
        public int CurrencyId { get; set; }
        /// <summary>
        /// 货币名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Taker订单费率
        /// </summary>
        public decimal TakerRate { get; set; }
        /// <summary>
        /// Maker订单费率
        /// </summary>
        public decimal MakerRate { get; set; }
        /// <summary>
        /// 默认利润单价
        /// </summary>
        public decimal DefaultProfit { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
