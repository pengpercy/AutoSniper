using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoSniper.Framework.Converter;
using AutoSniper.ClientWPF.Services.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Text;

namespace AutoSniper.UnitTest
{
    [TestClass]
    public class DataConvertTest
    {
        [TestMethod]
        public void TimeStampConvertTest()
        {
            var time = DateTime.Now;
            var result = time.ToTimeStamp().ToDateTime();
            Assert.IsTrue(time.ToTimeStamp() == result.ToTimeStamp());
        }
        [TestMethod]
        public void JsonConvertTest()
        {
            var json = @"{
    'currency': 'btc',
    'id': '20150928158614292',
    'price': 1560,
    'status': 3,
    'total_amount': 0.1,
    'trade_amount': 0,
    'trade_price' : 6000,
    'trade_date': 1443410396717,
    'trade_money': 0,
    'type': 0,
}";
            var model = json.FromJSON<OrderModel>();
            Assert.IsTrue(model.TradeType == TradeType.sell);

            json = @"[
    {
    'currency': 'btc',
    'id': '20150928158614292',
    'price': 1560,
    'status': 3,
    'total_amount': 0.1,
    'trade_amount': 0,
    'trade_price' : 6000,
    'trade_date': 1443410396717,
    'trade_money': 0,
    'type': 0,
}
]";
            var list = JArray.Parse(json).ToObject<List<OrderModel>>();
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void ConvertTest()
        {
            var json = @"
{
    'data': [
        [
            1472107500000,
            3840.46,
            3843.56,
            3839.58,
            3843.3,
            492.456
        ]
    ],
    'moneyType': 'cny',
    'symbol': 'btc'
}
";
            var data = json.FromJSON<KlineModel>();
            Assert.IsTrue(data.Data.Any());

            json = @"
{
    'asks': [
        [
            83.28,
            11.8
        ]
    ],
    'bids': [
        [
            81.91,
            3.65
        ]
    ],
    'timestamp' : 1472107500000
    }
";
            var data1 = json.FromJSON<DepthModel>();
            Assert.IsTrue(data1.Asks.Any() && data1.Bids.Any());
        }

        [TestMethod]
        public void JsonsConvertTest()
        {
            var url = $"";
            var json = url.GetJsonFromUrl();
            var obj = JObject.Parse(json)["result"];
            var data = obj["base"].ToObject<AccountModel>();
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
            Assert.IsTrue(data.AuthGoogleEnabled);
        }

    }
}
