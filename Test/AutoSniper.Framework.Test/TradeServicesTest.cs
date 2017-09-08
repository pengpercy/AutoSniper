using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoSniper.Framework.Converter;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.Repositories;
using System.Linq;
using AutoSniper.ClientWPF.Services;

namespace AutoSniper.UnitTest
{
    [TestClass]
    public class TradeServicesTest
    {
        [TestMethod]
        public void GetAccountInfoTest()
        {
            var json = TradeServices.GetAccountInfo();
            Assert.IsTrue(!string.IsNullOrWhiteSpace(json));
        }
    }
}
