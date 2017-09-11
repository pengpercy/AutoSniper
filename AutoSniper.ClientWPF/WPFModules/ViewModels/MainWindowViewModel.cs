using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.ClientWPF.WPFModules.Commands;
using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.WPFModules.Services;
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
                AssetInfo = AccountServices.GetAssetInfo(Currency.bcc_cny);
                //ActiveTrades = new ObservableCollection<TradeBookModel>(TradeOrderServices.GetActiveTrades());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AssetInfo = new AssetModel();
            }
        }

        private ObservableCollection<TradeBookModel> _activeTrades;
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

        private AssetModel _asset;
        /// <summary>
        /// 资产信息
        /// </summary>
        public AssetModel AssetInfo
        {
            get { return _asset; }
            set { _asset = value; }
        }

        //private bool _isConnected;
        //public bool IsConnected
        //{
        //    get { return _isConnected; }
        //    set
        //    {
        //        _isConnected = value;
        //        OnPropertyChanged();
        //    }
        //}

        private ICommand _getAssetCommand;
        public ICommand GetAssetCommand
        {
            get
            {
                if (_getAssetCommand == null)
                    _getAssetCommand = new RelayCommandAsync(() => AccountServices.GetAssetInfoAsync(Currency.bcc_cny));
                return _getAssetCommand;
            }
        }

        private async Task<AssetModel> GetAsset(Currency currency)
        {
            try
            {
                return await AccountServices.GetAssetInfoAsync(currency);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new AssetModel();
            }
        }
    }
}
