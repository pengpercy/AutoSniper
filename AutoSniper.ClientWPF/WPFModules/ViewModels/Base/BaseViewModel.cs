using AutoSniper.Framework.Logger;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoSniper.ClientWPF.WPFModules.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected ILog Logger;
        public BaseViewModel()
        {
            Logger = new Log4NetLogFactory().GetLog(GetType().Name);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName()] string name = null)
        {
            if (name != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
