using AutoSniper.ClientWPF.Repositories;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.ClientWPF.WPFModules.Models;
using AutoSniper.ClientWPF.Services;
using AutoSniper.Framework.Converter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoSniper.ClientWPF.WPFModules.Services
{
    public class TradeOrderServices
    {
        public static async Task<ObservableCollection<TradeBookModel>> GetActiveTradesAsync()
        {
            return await Task.Run(() => { return GetActiveTrades(); });
        }

        public static ObservableCollection<TradeBookModel> GetActiveTrades()
        {
            var list = TradeBookRepository.GetActiveOrder();
            Mapper.Initialize(m => m.CreateMap<TradeBook, TradeBookModel>());
            return Mapper.Map<IEnumerable<TradeBook>, ObservableCollection<TradeBookModel>>(list);
        }
    }
}
