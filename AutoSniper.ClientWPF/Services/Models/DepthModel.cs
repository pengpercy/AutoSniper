using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services.Models
{
    /// <summary>
    /// 市场深度
    /// </summary>
    public class DepthModel
    {
        /// <summary>
        /// 卖方深度 [价格，交易量]
        /// </summary>
        public decimal[][] Asks { get; set; }
        /// <summary>
        /// 买方深度 [价格，交易量]
        /// </summary>
        public decimal[][] Bids { get; set; }
        /// <summary>
        /// 此次深度的产生时间戳
        /// </summary>
        public long Timestamp { get; set; }
    }
}
