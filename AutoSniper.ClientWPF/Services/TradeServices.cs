using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.Framework.Converter;
using AutoSniper.Framework.Logger;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoSniper.ClientWPF.Services
{
    /// <summary>
    /// 交易操作
    /// </summary>
    public class TradeServices
    {
        private static ILog Logger = new Log4NetLogFactory().GetLog(typeof(TradeServices).Name);
        public static string BaseUrl = "https://trade.chbtc.com/api/";

        private static ApiKeyModel ApiKey;

        public static ApiKeyModel GetApiKey()
        {
            if (ApiKey != null) { return ApiKey; }
            var configPath = "../../AppData/AppConfig.json";
            ApiKey = File.Exists(configPath) ?
                JObject.Parse(File.ReadAllText(configPath))["apikey"].ToObject<ApiKeyModel>()
                : null;
            return ApiKey;
        }




        /// <summary>
        /// 获取用户资产以及安全认证设置信息
        /// </summary>
        /// <returns></returns>
        public static AccountModel GetAccountInfo()
        {
            var json = string.Empty;
            var method = "getAccountInfo";
            string url = BaseUrl + method;
            var data = new AccountModel();
            try
            {
                var queryString = new { method, GetApiKey().accesskey }.ToQueryString();
                url = url.MakeUrl(queryString, GetApiKey().secretkey);
                json = url.GetJsonFromUrl();
                var obj = JObject.Parse(json)["result"];
                data = obj["base"].ToObject<AccountModel>();
                data.TotalAssets = obj["totalAssets"].Value<decimal>();
                data.NetAssets = obj["netAssets"].Value<decimal>();
                data.AssetsList = new Dictionary<string, Assets>();
                Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList().ForEach(m =>
                {
                    var i = Enum.GetName(typeof(Currency), m).Replace("_cny", "").ToUpper();
                    var asset = obj["balance"][i].ToObject<Assets>();
                    asset.Symbol = HttpUtility.UrlDecode(asset.Symbol);
                    asset.FrozenAmount = obj["frozen"][i]["amount"].Value<decimal>();
                    //排除HSR
                    if (!new[] { "HSR", "QTUM" }.Contains(i))
                    {
                        asset.P2pInVol = obj["p2p"][$"in{i}"].Value<decimal>();
                        asset.P2pOutVol = obj["p2p"][$"out{i}"].Value<decimal>();
                    }
                    data.AssetsList.Add(i, asset);
                });
                return data;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return data;
        }

        /// <summary>
        /// 委托下单
        /// </summary>
        /// <param name="price">委托价格</param>
        /// <param name="amount">交易数量，不是cny金额</param>
        /// <param name="tradeType">交易类型，buy,sell</param>
        /// <param name="currency">货币类型</param>
        /// <returns></returns>
        public static ResponseModel Order(decimal price, decimal amount, TradeType tradeType, Currency currency)
        {
            var json = string.Empty;
            var method = "order";
            var url = BaseUrl + method;
            try
            {
                var queryString = new { method, GetApiKey().accesskey, price, amount, tradeType = (int)tradeType, currency }.ToQueryString();
                json = url.MakeUrl(queryString, GetApiKey().secretkey).GetJsonFromUrl();
                return json.FromJSON<ResponseModel>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new ResponseModel();
        }

        /// <summary>
        /// 取消委托
        /// </summary>
        /// <param name="orderId">委托挂单号</param>
        /// <param name="currency">货币类型</param>
        /// <returns></returns>
        public static ResponseModel CancelOrder(string orderId, Currency currency)
        {
            var json = string.Empty;
            var method = "cancelOrder";
            var url = BaseUrl + method;
            try
            {
                var queryString = new { method, GetApiKey().accesskey, id = orderId, currency }.ToQueryString();
                json = url.MakeUrl(queryString, GetApiKey().secretkey).GetJsonFromUrl();
                return json.FromJSON<ResponseModel>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new ResponseModel();
        }

        /// <summary>
        /// 获取委托买单或卖单
        /// </summary>
        /// <param name="orderId">委托挂单号</param>
        /// <param name="currency">交易类型（目前仅支持btc_cny/ltc_cny/eth_cny/eth_btc/etc_cny/bts_cny/eos_cny/bcc_cny/qtum_cny）</param>
        /// <returns></returns>
        public static OrderModel GetOrder(string orderId, Currency currency)
        {
            var json = string.Empty;
            var method = "getOrder";
            var url = BaseUrl + method;
            try
            {
                var queryString = new { method, GetApiKey().accesskey, id = orderId, currency }.ToQueryString();
                json = url.MakeUrl(queryString, GetApiKey().secretkey).GetJsonFromUrl();
                return json.FromJSON<OrderModel>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new OrderModel();
        }

        /// <summary>
        /// 获取多个委托买单或卖单，每次请求返回10条记录
        /// </summary>
        /// <param name="tradeType">交易类型1/0[buy/sell]</param>
        /// <param name="currency">交易类型（目前仅支持btc_cny/ltc_cny/eth_cny/eth_btc/etc_cny/bts_cny/eos_cny/bcc_cny/qtum_cny）</param>
        /// <param name="pageIndex">当前页数</param>
        /// <returns></returns>
        public static List<OrderModel> GetOrders(TradeType tradeType, Currency currency, int pageIndex)
        {
            var json = string.Empty;
            var method = "getOrders";
            var url = BaseUrl + method;
            try
            {
                var queryString = new { method, GetApiKey().accesskey, tradeType = (int)tradeType, currency, pageIndex }.ToQueryString();
                json = url.MakeUrl(queryString, GetApiKey().secretkey).GetJsonFromUrl();
                return JArray.Parse(json).ToObject<List<OrderModel>>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new List<OrderModel>();
        }

        /// <summary>
        /// (新)获取多个委托买单或卖单，每次请求返回pageSize小于100条记录
        /// </summary>
        /// <param name="tradeType">交易类型1/0[buy/sell]</param>
        /// <param name="currency">交易类型（目前仅支持btc_cny/ltc_cny/eth_cny/eth_btc/etc_cny/bts_cny/eos_cny/bcc_cny/qtum_cny）</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static List<OrderModel> GetOrdersNew(TradeType tradeType, Currency currency, int pageIndex, int pageSize)
        {
            var json = string.Empty;
            var method = "getOrdersNew";
            var url = BaseUrl + method;
            try
            {
                var queryString = new { method, GetApiKey().accesskey, tradeType = (int)tradeType, currency, pageIndex, pageSize }.ToQueryString();
                json = url.MakeUrl(queryString, GetApiKey().secretkey).GetJsonFromUrl();
                return JArray.Parse(json).ToObject<List<OrderModel>>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new List<OrderModel>();
        }

        /// <summary>
        /// 与getOrdersNew的区别是取消tradeType字段过滤，可同时获取买单和卖单，每次请求返回pageSize小于100条记录
        /// </summary>
        /// <param name="currency">交易类型（目前仅支持btc_cny/ltc_cny/eth_cny/eth_btc/etc_cny/bts_cny/eos_cny/bcc_cny/qtum_cny）</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static List<OrderModel> GetOrdersIgnoreTradeType(Currency currency, int pageIndex, int pageSize)
        {
            var json = string.Empty;
            var method = "getOrdersIgnoreTradeType";
            var url = BaseUrl + method;
            try
            {
                var queryString = new { method, GetApiKey().accesskey, currency, pageIndex, pageSize }.ToQueryString();
                json = url.MakeUrl(queryString, GetApiKey().secretkey).GetJsonFromUrl();
                return JArray.Parse(json).ToObject<List<OrderModel>>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new List<OrderModel>();
        }

        /// <summary>
        /// 获取未成交或部份成交的买单和卖单，每次请求返回pageSize小于等于10条记录
        /// </summary>
        /// <param name="currency">交易类型（目前仅支持btc_cny/ltc_cny/eth_cny/eth_btc/etc_cny/bts_cny/eos_cny/bcc_cny/qtum_cny）</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static List<OrderModel> GetUnfinishedOrdersIgnoreTradeType(Currency currency, int pageIndex, int pageSize)
        {
            var json = string.Empty;
            var method = "getUnfinishedOrdersIgnoreTradeType";
            var url = BaseUrl + method;
            try
            {
                var queryString = new { method, GetApiKey().accesskey, currency, pageIndex, pageSize }.ToQueryString();
                json = url.MakeUrl(queryString, GetApiKey().secretkey).GetJsonFromUrl();
                return JArray.Parse(json).ToObject<List<OrderModel>>();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat($"Response:{json},Parameters:{url},Ex:{ex}");
            }
            return new List<OrderModel>();
        }
    }
}
