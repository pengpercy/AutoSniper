using AutoSniper.ClientWPF.Repositories;
using AutoSniper.ClientWPF.Repositories.BaseProvider;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.ClientWPF.TradeCore;
using AutoSniper.ClientWPF.WPFModules.Commands;
using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.WPFModules.Services;
using AutoSniper.ClientWPF.WPFModules.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoSniper.ClientWPF.WPFModules.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            try
            {
                //初始化数据库连接
                DataProvider.GetConnection();
                //开启多线程，加快启动速度
                CreateTradeModel = new CreateTradeModel();
                Task.Run(() => { LoadData(); });
                Task.Run(() => CheckOrder());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                AssetInfo = new AssetModel();
            }
        }

        //轮询检查订单状态
        public void CheckOrder()
        {
            while (true)
            {
                var isFreshActiveOrderList = ProcessTrade.CheckTradeOrder(CurrencyType.bcc_cny);
                if (isFreshActiveOrderList)
                {
                    AssetInfo = AccountServices.GetAssetInfo(CurrencyType.bcc_cny);
                    ActiveTrades = TradeOrderServices.GetActiveTrades();
                }
                Thread.Sleep(TimeSpan.FromSeconds(2));
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
                    //更新活跃订单列表 
                    LoadData();
                    CreateTradeModel = new CreateTradeModel();
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public async void LoadData()
        {
            //更新资产面板
            AssetInfo = await AccountServices.GetAssetInfoAsync(CurrencyType.bcc_cny);
            ActiveTrades = await TradeOrderServices.GetActiveTradesAsync();
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
                    _cancelTradeOrder = new RelayCommand(param => CancelOrderAsync((TradeBookModel)param));
                }
                return _cancelTradeOrder;
            }
        }

        private void CancelOrderAsync(TradeBookModel tradeBook)
        {
            var orderId = TradeStatus.买单中 == tradeBook.Status ? tradeBook.BuyOrderId : tradeBook.SellOrderId;
            var result = TradeOrderServices.CancelTrade(CurrencyType.bcc_cny, tradeBook.TradeId, orderId);
            LoadData();
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