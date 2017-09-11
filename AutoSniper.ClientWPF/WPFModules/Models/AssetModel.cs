using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.WPFModules.Models
{
    public class AssetModel
    {
        private string _currency;
        /// <summary>
        /// 货币类型
        /// </summary>
        public string Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                AvailableBuyLabel = $"可买{value}";
                AvailableVolLabel = $"可用{value}";
            }
        }
        /// <summary>
        /// 可用CNY
        /// </summary>
        public decimal AvailableCNY { get; set; }
        /// <summary>
        /// 可买XXX
        /// </summary>
        public string AvailableBuyLabel { get; set; }
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
        /// 可用XXX
        /// </summary>
        public string AvailableVolLabel { get; set; }
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
