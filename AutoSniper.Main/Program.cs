using AutoSniper.Framework.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var data = QuotationServices.GetTicker();
            //Console.WriteLine("Time:{0} s", (DateTime.Now.ToTimeStamp() - data.Date) / 1000m);
            //Console.WriteLine("High:{0}", data.High);
            //Console.WriteLine("Low:{0}", data.Low);
            //Console.WriteLine("Buy:{0}", data.Buy);
            //Console.WriteLine("Sell:{0}", data.Sell);
            //Console.WriteLine("Last:{0}", data.Last);
            //Console.WriteLine("Vol:{0}", data.Vol);

            var data = TradeServices.GetAccountInfo();

            Console.WriteLine(data);

            Console.ReadKey();
        }
    }
}
