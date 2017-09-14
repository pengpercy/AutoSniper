using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.ClientWPF.WPFModules.Commands;
using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.WPFModules.Services;
using AutoSniper.ClientWPF.WPFModules.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoSniper.ClientWPF.WPFModules.ViewModels
{
    public class ActiveTradeControlModel : BaseViewModel
    {
        public ActiveTradeControlModel()
        {
            ActiveTrades = TradeOrderServices.GetActiveTrades();
        }

        /// <summary>
        /// 活跃的交易数据集合
        /// </summary>
        public ObservableCollection<TradeBookModel> ActiveTrades
        {
            get { return _activeTrades; }
            set
            {
                _activeTrades = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<TradeBookModel> _activeTrades;

        private TradeBookModel _selectItem;
        public TradeBookModel SelectItem
        {
            get { return _selectItem; }
            set
            {
                _selectItem = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        private ICommand _cancelTradeOrder;
        public ICommand CancelTradeOrderCommand
        {
            get
            {
                if (_cancelTradeOrder == null)
                {
                    _cancelTradeOrder = new RelayCommandAsync(() => CancelOrderAsync());
                }
                return _cancelTradeOrder;
            }
        }

        private async Task<bool> CancelOrderAsync()
        {
            var orderId = TradeStatus.买单中 == _selectItem.Status ? _selectItem.BuyOrderId : _selectItem.SellOrderId;
            var result = await TradeOrderServices.CancelTradeAsync(CurrencyType.bcc_cny, _selectItem.TradeId, orderId);
            if (result)
            {
                //更新活跃订单列表
                ActiveTrades = TradeOrderServices.GetActiveTrades();
                //TODO 更新资产面板
            }
            return result;
        }


        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual new void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
