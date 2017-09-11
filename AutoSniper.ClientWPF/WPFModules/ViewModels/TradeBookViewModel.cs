using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.WPFModules.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.WPFModules.ViewModels
{
    public class TradeBookViewModel : BaseViewModel
    {
        public TradeBookViewModel()
        {
            ActiveTrades = new ObservableCollection<TradeBookModel>(TradeOrderServices.GetActiveTrades());
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
    }
}
