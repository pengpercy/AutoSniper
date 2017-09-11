using AutoSniper.ClientWPF.Services;
using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.ClientWPF.WPFModules.Commands;
using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.WPFModules.Services;
using System;
using System.Collections.Generic;
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
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AssetInfo = new AssetModel();
            }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        private ICommand _getAssetCommand;
        public ICommand GetAssetCommand
        {
            get
            {
                if (_getAssetCommand == null)
                    _getAssetCommand = new RelayCommandAsync(() => GetAsset());
                return _getAssetCommand;
            }
        }

        private AssetModel _asset;
        public AssetModel AssetInfo
        {
            get { return _asset; }
            set { _asset = value; }
        }

        private async Task<AssetModel> GetAsset()
        {
            try
            {
                return await AccountServices.GetAssetInfoAsync(Currency.bcc_cny);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new AssetModel();
            }
        }
    }
}
