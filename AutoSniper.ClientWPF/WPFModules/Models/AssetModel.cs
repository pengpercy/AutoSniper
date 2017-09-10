using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.WPFModules.Models
{
    public class AssetModel
    {
        /// <summary>
        /// 可用CNY
        /// </summary>
        public decimal AvailableCNY { get; set; }
        /// <summary>
        /// 可买货币额度
        /// </summary>
        public decimal AvailableBuy { get; set; }
        /// <summary>
        /// 折合总资产(RMB)
        /// </summary>
        public decimal TotalAssets { get; set; }
        /// <summary>
        /// 净资产(RMB)
        /// </summary>
        public decimal NetAssets { get; set; }
        /// <summary>
        /// 可用货币额度
        /// </summary>
        public decimal AvailableVol { get; set; }
        /// <summary>
        /// 冻结CNY
        /// </summary>
        public decimal FrozenCNY { get; set; }
    }
}
