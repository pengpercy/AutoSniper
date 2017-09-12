﻿using AutoSniper.ClientWPF.Repositories;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.Services;
using AutoSniper.Framework.Converter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoSniper.ClientWPF.Services.Models;

namespace AutoSniper.ClientWPF.WPFModules.Services
{
    public class TradeOrderServices
    {
        public static async Task<ObservableCollection<TradeBookModel>> GetActiveTradesAsync()
        {
            return await Task.Run(() => { return GetActiveTrades(); });
        }

        public static ObservableCollection<TradeBookModel> GetActiveTrades()
        {
            var list = TradeBookRepository.GetActiveTrade();
            Mapper.Initialize(m => m.CreateMap<TradeBook, TradeBookModel>());
            return Mapper.Map<IEnumerable<TradeBook>, ObservableCollection<TradeBookModel>>(list);
        }

        /// <summary>
        /// 异步创建交易订单
        /// </summary>
        /// <param name="currency">交易货币类型</param>
        /// <param name="price">价格</param>
        /// <param name="volume">交易量</param>
        /// <param name="profitPrice">利润单价</param>
        /// <returns></returns>
        public static async Task<bool> CreateTradeAsync(CurrencyType currency, decimal price, decimal volume, decimal profitPrice)
        {
            return await Task.Run(() => { return CreateTrade(currency, price, volume, profitPrice); });
        }

        /// <summary>
        /// 创建交易订单
        /// </summary>
        /// <param name="currency">交易货币类型</param>
        /// <param name="price">价格</param>
        /// <param name="volume">交易量</param>
        /// <param name="profitPrice">利润单价</param>
        /// <returns></returns>
        public static bool CreateTrade(CurrencyType currency, decimal price, decimal volume, decimal profitPrice)
        {
            if (volume * price > 10)
            {
                throw new Exception("主人很穷，测试下单就不要搞那么大数据了吧！");
            }


            var tradeId = 0;
            var result = TradeServices.Order(price, volume, TradeType.buy, currency);
            if (result.Code == "1000")
            {
                var tradeBook = new TradeBook();
                tradeBook.BuyOrderId = result.Id;
                tradeBook.BuyPrice = price;
                tradeBook.BuyVolume = volume;
                tradeBook.BuyAmount = volume * price;
                tradeBook.BuyTradePrice = 0;
                tradeBook.BuyTradeVolume = 0;
                tradeBook.SellOrderId = "";
                tradeBook.SellPrice = 0;
                tradeBook.SellVolume = 0;
                tradeBook.SellAmount = 0;
                tradeBook.SellTradeVolume = 0;
                tradeBook.SellTradePrice = 0;
                tradeBook.Profit = profitPrice;
                tradeBook.Status = TradeStatus.买单中.ToString();
                tradeBook.UpdateDate = DateTime.Now.ToString();
                tradeBook.CreateDate = DateTime.Now.ToString();
                tradeId = TradeBookRepository.CrateTrade(tradeBook);
            }
            return result.Code == "1000" && tradeId > 0;
        }
    }
}

