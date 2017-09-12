using AutoSniper.ClientWPF.Repositories;
using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.ClientWPF.WPFModules.Commands;
using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.WPFModules.Services;
using AutoSniper.ClientWPF.WPFModules.Utility;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                AssetInfo = AccountServices.GetAssetInfo(CurrencyType.bcc_cny);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AssetInfo = new AssetModel();
            }
        }

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

        private AssetModel _asset;
        /// <summary>
        /// 资产信息
        /// </summary>
        public AssetModel AssetInfo
        {
            get { return _asset; }
            set
            {
                _asset = value;
                OnPropertyChanged();
            }
        }

        private decimal? _price;
        public decimal? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private decimal? _volume;
        public decimal? Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged();
            }
        }

        private string _amount;
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }





        private ICommand _createTradeOrderCommand;
        public ICommand CreateTradeOrderCommand
        {
            get
            {
                if (_createTradeModel == null)
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
                bool result = await Task.Run(() => TradeOrderServices.CreateTrade(CurrencyType.bcc_cny, _price ?? 0, _volume ?? 0, CurrencyRepository.GetCurrency("BCC").DefaultProfit));
                if (result)
                {
                    //操作成功，更新资产面板
                    AssetInfo = AccountServices.GetAssetInfo(CurrencyType.bcc_cny);
                    //ServiceLocator.Current.GetInstance<TradeBookViewModel>().ActiveTrades = TradeOrderServices.GetActiveTrades(); 
                    //new TradeBookViewModel().ActiveTrades = TradeOrderServices.GetActiveTrades();
                    new ViewModelLocator().TradeBookVM.ActiveTrades = TradeOrderServices.GetActiveTrades();
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }



        private ICommand _getAssetCommand;
        public ICommand GetAssetCommand
        {
            get
            {
                if (_getAssetCommand == null)
                    _getAssetCommand = new RelayCommandAsync(() => AccountServices.GetAssetInfoAsync(CurrencyType.bcc_cny));
                return _getAssetCommand;
            }
        }
    }
}
