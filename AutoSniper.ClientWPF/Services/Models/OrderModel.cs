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
    /// getOrder返回数据信息
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// 交易类型（目前仅支持btc_cny/ltc_cny/eth_cny/eth_btc/etc_cny/bts_cny/eos_cny/bcc_cny/qtum_cny）
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 委托挂单号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 挂单状态(0：待成交, 1：取消, 2：交易完成, 3：待成交未交易部份)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 挂单总数量
        /// </summary>
        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        ///  已成交数量
        /// </summary>
        [JsonProperty("trade_amount")]
        public decimal TradeAmount { get; set; }
        /// <summary>
        /// 委托时间
        /// </summary>
        [JsonProperty("trade_date")]
        public long TradeDate { get; set; }
        /// <summary>
        /// 已成交总金额
        /// </summary>
        [JsonProperty("trade_money")]
        public decimal TradeMoney { get; set; }
        /// <summary>
        /// 成交均价
        /// </summary>
        [JsonProperty("trade_price")]
        public decimal TradePrice { get; set; }
        /// <summary>
        /// 挂单类型 1/0[buy/sell]
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        /// </summary>
        public TradeType TradeType { get; set; }
    }
}
