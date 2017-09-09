using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services.Models
{
    /// <summary>
    /// K线数据
    /// </summary>
    public class KlineModel
    {
        /// <summary>
        /// K线内容 [时间戳,开,高,低,收,交易量]
        /// </summary>
        public decimal[][] Data { get; set; }
        /// <summary>
        /// 买入货币
        /// </summary>
        public string MoneyType { get; set; }
        /// <summary>
        /// 卖出货币
        /// </summary>
        public string Symbol { get; set; }
    }
}
