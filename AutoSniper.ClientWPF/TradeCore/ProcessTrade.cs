using AutoSniper.ClientWPF.Repositories;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.Services;
using AutoSniper.ClientWPF.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.TradeCore
{
    public class ProcessTrade
    {
        public static Services.Models.Currency currency = Services.Models.Currency.bcc_cny;
        public static void CheckTradeOrder()
        {
            var localActiveOrders = TradeBookRepository.GetActiveOrder();
            var remoteActiveOrders = GetRemoteActiveOrder(currency);
            var currencSetting = CurrencyRepository.GetCurrency(currency.ToString().Replace("_cny", "").ToUpper());
            foreach (var localOrder in localActiveOrders)
            {
                var remoteOrder = remoteActiveOrders.First(q => q.Id == localOrder.BuyOrderId || q.Id == localOrder.SellOrderId);
                if (remoteOrder == null) continue;
                if (remoteOrder.Status == 2 && remoteOrder.TradeType == TradeType.buy && localOrder.Status != TradeStatus.买单中.ToString())
                {
                    //ToDo 发送卖单请求，数据库更新状态

                    //买单手续费= 已成交量 * 成交均价 * 委托挂单费率；
                    var buyFee = remoteOrder.TradeAmount * remoteOrder.TradePrice * currencSetting.MakerRate;
                    //var sellPrice = BudgetaryPrice(volume);

                }

            }
        }

        /// <summary>
        /// 根据买单成本预算卖单价格
        /// </summary>
        /// <param name="volume">成交量</param>
        /// <param name="priceProfit">利润单价</param>
        /// <param name="buyTradePrice">买单成交价</param>
        /// <param name="buyFee">买单手续费</param>
        /// <param name="makerRate">卖单费率</param>
        /// <returns>预测卖单价</returns>
        public static decimal BudgetaryPrice(decimal volume, decimal priceProfit, decimal buyTradePrice, decimal buyFee, decimal makerRate)
        {
            return ((buyTradePrice + priceProfit) * volume + buyFee) / (volume * (1 - makerRate));
        }


        /// <summary>
        /// 从服务器获取所有活跃订单
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static List<OrderModel> GetRemoteActiveOrder(Services.Models.Currency currency)
        {
            List<OrderModel> activeOrders = new List<OrderModel>();
            var pageIndex = 1;
            var flag = true;
            while (flag)
            {
                var list = TradeServices.GetUnfinishedOrdersIgnoreTradeType(currency, pageIndex, 10);
                flag = list.Count >= 10;
                activeOrders.ForEach(s => { list.RemoveAll(q => q.Id == s.Id); });
                if (!list.Any()) { continue; }
                activeOrders.AddRange(list);
                pageIndex += 1;
            }
            return activeOrders;

        }

    }
}
