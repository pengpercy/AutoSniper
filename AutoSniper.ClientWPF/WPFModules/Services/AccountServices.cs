using AutoSniper.ClientWPF.Services;
using AutoSniper.ClientWPF.Services.Models;
using AutoSniper.ClientWPF.WPFModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.WPFModules.Services
{
    public class AccountServices
    {
        public static async Task<AssetModel> GetAssetInfoAsync(Currency currency)
        {
            return await Task.Run(() => { return GetAssetInfo(currency); });
        }

        public static AssetModel GetAssetInfo(Currency currency)
        {
            var asset = new AssetModel();
            var account = TradeServices.GetAccountInfo();
            asset.Currency = currency.ToString().Replace("_cny", "").ToUpper();
            var currentAsset = account.AssetsList[asset.Currency];
            asset.AvailableVol = currentAsset.Amount;
            asset.NetAssets = account.NetAssets;
            asset.TotalAssets = account.TotalAssets;
            asset.AvailableBuy = 0;
            asset.FrozenCNY = account.AssetsList["CNY"].FrozenAmount;
            asset.AvailableCNY = account.AssetsList["CNY"].Amount;
            return asset;
        }
    }
}
