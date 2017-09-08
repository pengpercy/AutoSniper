using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.Framework.Converter;
using AutoSniper.Framework.Logger;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services
{
    /// <summary>
    /// 行情信息查询
    /// </summary>
    public class QuotationServices
    {
        private static ILog Logger = new Log4NetLogFactory().GetLog(typeof(QuotationServices).Name);
        public static string BaseUrl = "http://api.chbtc.com/data/v1/";
        /// <summary>
        /// 货币类型
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static TickerModel GetTicker(Currency currency)
        {
            var json = string.Empty;
            var url = BaseUrl + $"ticker?currency={currency}";
            try
            {
                json = url.GetJsonFromUrl();
                var model = JObject.Parse(json)["ticker"].ToObject<TickerModel>();
                model.Date = JObject.Parse(json)["date"].ToObject<long>().ToDateTime();
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
