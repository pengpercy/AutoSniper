using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoSniper.Framework.Converter;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.Repositories;
using System.Linq;
using AutoSniper.ClientWPF.Services;
using Newtonsoft.Json.Linq;
using AutoSniper.ClientWPF.Services.Models;
using CurrencyEnum = AutoSniper.ClientWPF.Services.Models.Currency;
using System.Collections.Generic;
using System.Web;

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
    }
}
