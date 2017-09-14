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

namespace AutoSniper.ClientWPF.WPFModules.ViewModels
{
    public class TradeBookViewModel : BaseViewModel
    {
        public TradeBookViewModel()
        {
            ActiveTrades = TradeOrderServices.GetActiveTrades();
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

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual new void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
