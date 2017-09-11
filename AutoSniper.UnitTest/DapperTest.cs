using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoSniper.Framework.Converter;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.Repositories;
using System.Linq;

namespace AutoSniper.UnitTest
{
    [TestClass]
    public class DapperTest
    {
        [TestMethod]
        public void InsertTest()
        {
            var trade = new TradeBook();
            trade.BuyOrderId = "235245213353431";
            trade.BuyPrice = 13;
            trade.BuyVolume = 1;
            trade.BuyTradeVolume = 0;
            trade.BuyTradePrice = 0;
            trade.BuyAmount = 0;
            trade.SellOrderId = "";
            trade.SellPrice = 3;
            trade.SellVolume = 0;
            trade.SellTradeVolume = 0;
            trade.SellTradePrice = 0;
            trade.SellAmount = 0;
            trade.Profit = 1;
            trade.Status = TradeStatus.买单中.ToString();
            trade.UpdateDate = DateTime.Now.ToString();
            trade.CreateDate = DateTime.Now.ToString();
            var result = TradeBookRepository.BuyOrder(trade);
            //var result = TradeBookRepository.CreateTradeBook2(trade);
            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var result = TradeBookRepository.CancelOrder(1);
            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void GetOrderTest()
        {
            var result = TradeBookRepository.GetActiveOrder().ToJson();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CurrentFunctionNameTest()
        {
            var name = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Assert.IsTrue(name == "CurrentFunctionNameTest");
        }

        [TestMethod]
        public void GetAllCurrencyTest()
        {
            var data = CurrencyRepository.GetAllCurrency();
            Assert.IsTrue(data.Any());
        }
    }
}
