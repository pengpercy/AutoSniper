using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using AutoSniper.ClientWPF.Services;
using AutoSniper.ClientWPF.WPFModules.Services;
using AutoSniper.ClientWPF.TradeCore;
using AutoSniper.ClientWPF.Services.Models;

namespace AutoSniper.UnitTest
{
    [TestClass]
    public class TradeServicesTest
    {
        [TestMethod]
        public void AccountInfoTest()
        {
            var data = TradeServices.GetAccountInfo();
            Assert.IsTrue(data.AuthGoogleEnabled);
        }

        [TestMethod]
        public void CreateTradeTest()
        {
            var result = TradeOrderServices.CreateTrade(CurrencyType.bcc_cny, 10, 0.001m, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetRemoteActiveOrderTest()
        {
            var list = TradeServices.GetOrdersIgnoreTradeType(CurrencyType.bcc_cny, 1, 99);
            Assert.IsTrue(list.Any());
        }
    }
}
