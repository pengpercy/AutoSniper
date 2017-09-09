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
    /// 获取CHBTC市场行情信息
    /// </summary>
    public class QuotationServices
    {
        private static ILog Logger = new Log4NetLogFactory().GetLog(typeof(QuotationServices).Name);
        public static string BaseUrl = "http://api.chbtc.com/data/v1/";

        /// <summary>
        /// 当前行情
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
                model.Date = JObject.Parse(json)["date"].ToObject<long>();
                return model;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new TickerModel();
        }

        /// <summary>
        /// 获取市场深度
        /// </summary>
        /// <param name="currency">火币类型</param>
        /// <param name="size">档位1-50，如果有合并深度，只能返回5档深度</param>
        /// <param name="merge">
        /// 合并深度:
        ///          btc_cny : 可选1,0.1
        ///          ltc_cny : 可选0.5,0.3,0.1
        ///          eth_cny : 可选0.5,0.3,0.1
        ///          etc_cny : 可选0.3,0.1
        ///          bts_cny : 可选1,0.1,0.01
        ///          eos_cny : 可选1,0.1,0.01
        ///          bcc_cny : 可选1,0.1
        ///          qtum_cny : 可选1,0.1,0.01
        ///          hsr_cny : 可选1,0.1,0.0
        /// </param>
        /// <returns></returns>
        public static DepthModel GetDepth(Currency currency, int size, decimal merge)
        {
            var json = string.Empty;
            var url = BaseUrl + $"depth?currency={currency}&size={size}&merge={merge}";
            try
            {
                json = url.GetJsonFromUrl();
                return json.FromJSON<DepthModel>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new DepthModel();
        }

        /// <summary>
        /// 获取历史成交数据, 从指定交易ID后50条数据
        /// </summary>
        /// <param name="currency">火币类型</param>
        /// <returns></returns>
        public static List<TradesModel> GetTrades(Currency currency)
        {
            var json = string.Empty;
            var url = BaseUrl + $"trades?currency={currency}";
            try
            {
                json = url.GetJsonFromUrl();
                return JArray.Parse(json).ToObject<List<TradesModel>>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new List<TradesModel>();
        }

        /// <summary>
        /// 获取k线数据
        /// </summary>
        /// <param name="currency">货币类型</param>
        /// <param name="since">从这个时间戳之后的</param>
        /// <param name="size">返回数据的条数限制(默认为1000，如果返回数据多于1000条，那么只返回1000条)</param>
        /// <returns></returns>
        public static KlineModel GetKline(Currency currency, long since = 1417536000000, int size = 1000)
        {
            var json = string.Empty;
            var url = BaseUrl + $"kline?currency={currency}&since={since}&size={size}";
            try
            {
                json = url.GetJsonFromUrl();
                return json.FromJSON<KlineModel>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new KlineModel();
        }
    }
}
