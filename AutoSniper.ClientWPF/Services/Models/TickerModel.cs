using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TickerModel
    {
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public decimal Last { get; set; }
        public decimal Vol { get; set; }
        public DateTime Date { get; set; }
    }
}
