using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace AutoSniper.ClientWPF.WPFModules.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (!new Regex(@"^\d+.?\d*$").IsMatch(textBox.Text))
            {
                textBox.Text = "";
            }
            var price = 0m;
            var volume = 0m;
            var amount = 0m;
            if (decimal.TryParse(BuyPrice.Text, out price) && decimal.TryParse(Volume.Text, out volume))
            {
                amount = price * volume;
                if (amount > 0)
                {
                    TradeAmount.Text = amount.ToString();
                    return;
                }
            }
            TradeAmount.Text = "";
        }
    }
}
