using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Repositories.Models
{
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
