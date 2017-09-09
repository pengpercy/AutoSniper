using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoSniper.Framework.Converter;
using AutoSniper.ClientWPF.Services.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

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
    }
}
