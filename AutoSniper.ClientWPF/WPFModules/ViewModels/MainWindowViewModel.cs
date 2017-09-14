using AutoSniper.ClientWPF.Repositories;
using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.ClientWPF.WPFModules.Commands;
using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.WPFModules.Services;
using AutoSniper.ClientWPF.WPFModules.Utility;
using AutoSniper.ClientWPF.WPFModules.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AutoSniper.ClientWPF.WPFModules.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            try
            {
                CreateTradeModel = new CreateTradeModel();
                AssetInfo = AccountServices.GetAssetInfo(CurrencyType.bcc_cny);
                ActiveTradeControlModel = new ActiveTradeControlModel();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AssetInfo = new AssetModel();
            }
        }

        /// <summary>
        /// 活跃订单数据集合
        /// </summary>
        private ActiveTradeControlModel _activeTradeControlModel;
        public ActiveTradeControlModel ActiveTradeControlModel
        {
            get { return _activeTradeControlModel; }
            set
            {
                _activeTradeControlModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 创建交易订单Form表单
        /// </summary>
        private CreateTradeModel _createTradeModel;
        public CreateTradeModel CreateTradeModel
        {
            get { return _createTradeModel; }
            set
            {
                _createTradeModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 资产面板数据信息
        /// </summary>
        private AssetModel _asset;
        public AssetModel AssetInfo
        {
            get { return _asset; }
            set
            {
                _asset = value;
                OnPropertyChanged();
            }
        }

        private ICommand _createTradeOrderCommand;
        public ICommand CreateTradeOrderCommand
        {
            get
            {
                if (_createTradeOrderCommand == null)
                {
                    _createTradeOrderCommand = new RelayCommandAsync(() => CreateTradeAsync());
                }
                return _createTradeOrderCommand;
            }
        }

        private async Task<bool> CreateTradeAsync()
        {
            try
            {
                decimal price;
                decimal volume;
                if (!decimal.TryParse(_createTradeModel.Price, out price) ||
                    !decimal.TryParse(_createTradeModel.Volume, out volume))
                { return false; }
                bool result = await TradeOrderServices.CreateTradeAsync(CurrencyType.bcc_cny, price, volume, CurrencyRepository.GetCurrency("BCC").DefaultProfit);
                if (result)
                {
                    //更新资产面板
                    AssetInfo = await AccountServices.GetAssetInfoAsync(CurrencyType.bcc_cny);
                    //更新活跃订单列表
                    ActiveTradeControlModel = new ActiveTradeControlModel();
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        #region Event Handle
        //public new event PropertyChangedEventHandler PropertyChanged;

        //protected virtual new void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        #endregion
    }
}