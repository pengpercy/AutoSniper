using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoSniper.Framework.Converter;

namespace AutoSniper.Framework.Test
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
    }
}
