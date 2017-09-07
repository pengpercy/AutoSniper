using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoSniper.Framework.Converter;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.Repositories;

namespace AutoSniper.UnitTest
{
    [TestClass]
    public class DapperTest
    {
        [TestMethod]
        public void InsertTest()
        {
            var trade = new TradeBook();
            trade.BuyOrderId = "23524521335";
            trade.BuyPrice = 13;
            trade.BuyVolume = 1;
            trade.BuyCompletedVolume = 0;
            trade.BuyAmount = 0;
            trade.SellOrderId = "";
            trade.SellPrice = 3;
            trade.SellVolume = 0;
            trade.SellCompletedVolume = 0;
            trade.SellAmount = 0;
            trade.Profit = 1;
            trade.Status = TradeStatus.Buying;
            trade.UpdateDate = DateTime.Now;
            trade.CreateDate = DateTime.Now;
            //var result = TradeBookRepository.CreateTradeBook(trade);
            var result = TradeBookRepository.CreateTradeBook2(trade);
            Assert.IsTrue(result == 1);
        }
    }
}
