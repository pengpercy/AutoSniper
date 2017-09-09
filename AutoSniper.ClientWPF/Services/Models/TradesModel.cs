using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services.Models
{
    /// <summary>
    /// 历史成交数据
    /// </summary>
    public class TradesModel
    {
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易时间戳
        /// </summary>
        public long Date { get; set; }
        /// <summary>
        /// 交易价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [JsonProperty("trade_type")]
        public string TradeType { get; set; }
        /// <summary>
        /// 交易类型，buy(买)/sell(卖)
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TradeType Type { get; set; }
    }
}
