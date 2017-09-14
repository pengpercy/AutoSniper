using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using AutoSniper.ClientWPF.WPFModules.ViewModels;

namespace AutoSniper.ClientWPF.WPFModules.Utility
{
    public class ViewModelLocator
    {
        private UnityContainer container;

        public ViewModelLocator()
        {
            container = new UnityContainer();
            //container.RegisterType<IChatService, ChatService>();
            //container.RegisterType<IDialogService, DialogService>();
        }

        public MainWindowViewModel MainVM
        {
            get { return container.Resolve<MainWindowViewModel>(); }
        }

        public TradeBookViewModel TradeBookVM
        {
            get { return container.Resolve<TradeBookViewModel>(); }
        }

        public ActiveTradeControlModel ActiveTradeVM
        {
            get { return container.Resolve<ActiveTradeControlModel>(); }
        }
    }
}
