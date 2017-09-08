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
    }
}
