using AutoSniper.Framework.Converter;
using AutoSniper.Framework.Logger;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Services
{
    public class TradeServices
    {
        private static ILog Logger = new Log4NetLogFactory().GetLog(typeof(TradeServices).Name);
        public static string BaseUrl = "https://trade.chbtc.com/api/";
        public static string accesskey = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
        public static string secretkey = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";

        public static string GetAccountInfo()
        {
            var json = string.Empty;
            var url = BaseUrl + "getAccountInfo";
            try
            {
                var queryString = new { method = "getAccountInfo", accesskey }.ToQueryString();
                var sign = SignUtility.MakeSign(queryString, secretkey);
                url = url + string.Format("?{0}&sign={1}&reqTime={2}", queryString, sign, DateTime.UtcNow.ToTimeStamp());
                json = url.GetJsonFromUrl();
                return json;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Ex:{0},Response:{1},Parameters:{2}", ex, json, url);
            }
            return "";
        }

    }
}
