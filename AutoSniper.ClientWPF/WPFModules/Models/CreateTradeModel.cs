using AutoSniper.ClientWPF.WPFModules.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.WPFModules.Models
{
    /// <summary>
    /// 创建交易订单
    /// </summary>
    public class CreateTradeModel : BaseViewModel
    {
        /// <summary>
        /// 委托价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 委托数量
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// 交易额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
