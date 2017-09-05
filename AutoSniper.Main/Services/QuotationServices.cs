using AutoSniper.Framework.Converter;
using AutoSniper.Framework.Logger;
using AutoSniper.Main.Services.Models;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.Main
{
    public class QuotationServices
    {
        private static ILog Logger = new Log4NetLogFactory().GetLog(typeof(QuotationServices).Name);
        public static string BaseUrl = "http://api.chbtc.com/data/v1/";
        public static TickerModel GetTicker()
        {
            var json = string.Empty;
            var url = BaseUrl + "ticker?currency=hsr_cny";
            try
            {
                json = url.GetJsonFromUrl();
                var model = JObject.Parse(json)["ticker"].ToObject<TickerModel>();
                model.Date = JObject.Parse(json)["date"].ToObject<long>();
                return model;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Ex:{0},Response:{1},Parameters:{2}", ex, json, url);
            }
            return new TickerModel();
        }
    }
}
