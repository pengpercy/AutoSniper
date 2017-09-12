using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services.Models
{
    /// <summary>
    /// 交易类型
    /// </summary>
    public enum TradeType
    {
        buy = 1,
        sell = 0
    }

    /// <summary>
    /// 货币类型
    /// </summary>
    public enum CurrencyType
    {
        cny = 0,
        btc_cny = 1,
        ltc_cny = 2,
        eth_cny = 3,
        etc_cny = 4,
        bts_cny = 5,
        eos_cny = 6,
        bcc_cny = 7,
        qtum_cny = 8,
        hsr_cny = 9
    }
}
