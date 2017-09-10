using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services.Models
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 是否开通谷歌验证
        /// </summary>
        [JsonProperty("auth_google_enabled")]
        public bool AuthGoogleEnabled { get; set; }
        /// <summary>
        /// 是否开通手机验证
        /// </summary>
        [JsonProperty("auth_mobile_enabled")]
        public bool AuthMobileEnabled { get; set; }
        /// <summary>
        /// 是否开通交易密码
        /// </summary>
        [JsonProperty("trade_password_enabled")]
        public bool TradePasswordEnabled { get; set; }
        /// <summary>
        /// 折合总资产(RMB)
        /// </summary>
        public decimal TotalAssets { get; set; }
        /// <summary>
        /// 净资产(RMB)
        /// </summary>
        public decimal NetAssets { get; set; }
        /// <summary>
        /// 资产数据，根据枚举 Currency 为顺序存储
        /// </summary>
        public Dictionary<string, Assets> AssetsList { get; set; }
    }

    /// <summary>
    /// 资产信息
    /// </summary>
    public class Assets
    {
        /// <summary>
        /// 余额（不含冻结）
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 货币符号
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// 冻结额度
        /// </summary>
        public decimal FrozenAmount { get; set; }
        /// <summary>
        /// P2P已借入货币量（额度）
        /// </summary>
        public decimal P2pInVol { get; set; }
        /// <summary>
        /// P2P已借出货币量（额度）
        /// </summary>
        public decimal P2pOutVol { get; set; }
    }
}
