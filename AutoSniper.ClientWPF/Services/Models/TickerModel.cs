using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services.Models
{
    /// <summary>
    /// 行情
    /// </summary>
    public class TickerModel
    {
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// 买一价
        /// </summary>
        public decimal Buy { get; set; }
        /// <summary>
        /// 卖一价
        /// </summary>
        public decimal Sell { get; set; }
        /// <summary>
        /// 最新成交价
        /// </summary>
        public decimal Last { get; set; }
        /// <summary>
        /// 成交量(最近的24小时)
        /// </summary>
        public decimal Vol { get; set; }
        /// <summary>
        /// 服务器时间,毫秒
        /// </summary>
        public long Date { get; set; }
    }
}
