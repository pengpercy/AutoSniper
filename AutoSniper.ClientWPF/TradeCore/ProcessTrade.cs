﻿using AutoSniper.ClientWPF.Repositories;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.Services;
using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.Framework.Converter;
using AutoSniper.Framework.Logger;
using System;
using System.Linq;

namespace AutoSniper.ClientWPF.TradeCore
{
    /// <summary>
    /// 定时任务，不断检查服务器中订单的成交情况，
    /// 根据买单成交状态，及时委托卖单，保证手中不持数据货币
    /// </summary>
    public class ProcessTrade
    {
        public static ILog Logger = new Log4NetLogFactory().GetLog(typeof(ProcessTrade).Name);

        public static bool CheckTradeOrder(CurrencyType currency)
        {
            //是否更新过订单状态
            bool enableUpdate = false;
            bool isUpdate = false;
            var localActiveOrders = TradeBookRepository.GetActiveTrade();
            var remoteActiveOrders = TradeServices.GetOrdersIgnoreTradeType(currency, 1, 100);
            var currencSetting = CurrencyRepository.GetCurrency(currency.ToString().Replace("_cny", "").ToUpper());
            foreach (var lOrder in localActiveOrders)
            {
                var rOrder = remoteActiveOrders.FirstOrDefault(q => q.Id == lOrder.BuyOrderId || q.Id == lOrder.SellOrderId);
                try
                {
                    //如果匹配不到，则直接通过订单Id查询远程订单详情
                    var orderId = TradeStatus.买单中.ToString() == lOrder.Status ? lOrder.BuyOrderId : lOrder.SellOrderId;
                    rOrder = rOrder ?? TradeServices.GetOrder(orderId, currency);
                    //如果最后还是匹配不到，则说明不是通过助手创建的订单，不做任何处理，直接跳过。
                    if (rOrder == null) { continue; };
                    //无论买单、卖单，如果是部分交易完成，则直接取消远程订单
                    if (rOrder.Status == 3)
                    {
                        var result = TradeServices.CancelOrder(rOrder.Id, currency);
                        if (result.Code != "1000")
                        {
                            Logger.Info($"远程订单[{rOrder.Id}]取消失败,返回结果：{result.ToJson()}");
                        }
                    }
                    if (new[] { 2, 3 }.Contains(rOrder.Status) && rOrder.TradeType == TradeType.buy && lOrder.Status == TradeStatus.买单中.ToString())
                    {
                        //公式： 买单手续费= 已成交量 * 成交均价 * 委托挂单费率；
                        var buyFee = rOrder.TradeAmount * rOrder.TradePrice * currencSetting.MakerRate;
                        //预估卖单价格
                        var sellPrice = BudgetaryPrice(rOrder.TradeAmount, currencSetting.DefaultProfit, rOrder.Price, buyFee, currencSetting.MakerRate);

                        var result = TradeServices.Order(sellPrice, rOrder.TradeAmount, TradeType.sell, currency);
                        if (result.Code == "2009")
                        {
                            Logger.Info($"账户余额不足，取消订单[{lOrder.TradeId}],返回结果：{result.ToJson()}");
                            TradeBookRepository.CancelOrder(lOrder.TradeId ?? 0);
                            enableUpdate = true;
                            continue;
                        }
                        if (result.Code != "1000")
                        {
                            Logger.Info($"创建远程卖单失败,对应本地订单[{lOrder.TradeId}],返回结果：{result.ToJson()}");
                            continue;
                        }
                        lOrder.BuyTradeVolume = rOrder.TradeAmount;
                        lOrder.BuyTradePrice = rOrder.TradePrice;
                        lOrder.BuyTradeAmount = rOrder.TradeAmount * rOrder.TradePrice;
                        lOrder.SellOrderId = result.Id;
                        lOrder.SellPrice = sellPrice;
                        lOrder.SellVolume = rOrder.TradeAmount;
                        lOrder.Status = TradeStatus.卖单中.ToString();
                        enableUpdate = true;
                        isUpdate = true;

                    }
                    else if (new[] { 2, 3 }.Contains(rOrder.Status) && rOrder.TradeType == TradeType.sell && lOrder.Status == TradeStatus.卖单中.ToString())
                    {
                        lOrder.SellTradePrice = rOrder.Price;
                        lOrder.SellTradeVolume = rOrder.TradeAmount;
                        lOrder.SellTradeAmount = rOrder.Price * rOrder.TradeAmount;
                        lOrder.Status = TradeStatus.已完成.ToString();
                        enableUpdate = true;
                        isUpdate = true;
                    }
                    if (rOrder.Status == 1)
                    {
                        lOrder.Status = TradeStatus.已取消.ToString();
                        TradeBookRepository.CancelOrder(lOrder.TradeId ?? 0);
                        enableUpdate = true;
                        //isUpdate = true;
                    }
                    if (isUpdate)
                    {
                        //根据匹配结果，更新本地订单状态
                        if (TradeBookRepository.UpdateOrder(lOrder) != 1)
                        {
                            Logger.Error($"更新本地订单[{lOrder.TradeId}]状态失败:买单中==>卖单中");
                        }
                        isUpdate = false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"LocalOrder:{lOrder.ToJson()},RemoteOrder:{rOrder.ToJson()},Ex:{ex.ToJson()}");
                }
            }
            return enableUpdate;
        }

        /// <summary>
        /// 根据买单成本预算卖单价格
        /// </summary>
        /// <param name="volume">买单成交量</param>
        /// <param name="priceProfit">利润单价</param>
        /// <param name="buyTradePrice">买单成交价</param>
        /// <param name="buyFee">买单手续费</param>
        /// <param name="makerRate">卖单费率</param>
        /// <returns>预测卖单价</returns>
        public static decimal BudgetaryPrice(decimal volume, decimal priceProfit, decimal buyTradePrice, decimal buyFee, decimal makerRate)
        {
            //公式：买单成交量 * 卖单交易均价 = 买单成交均价 * 买单成交量 + 买单手续费 + (买单成交量 * 卖单交易均价)*卖单手续费率
            return ((buyTradePrice + priceProfit) * volume + buyFee) / (volume * (1 - makerRate));
        }

    }
}
