using AutoSniper.ClientWPF.WPFModules.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.WPFModules.Models
{
    /// <summary>
    /// 创建交易订单
    /// </summary>
    public class CreateTradeModel
    {
        public string Price { get; set; }

        public string Volume { get; set; }

        public string Amount { get; set; }

    }
}
