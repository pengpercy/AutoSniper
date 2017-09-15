using AutoSniper.ClientWPF.Repositories.BaseProvider;
using AutoSniper.ClientWPF.Repositories.Models;
using AutoSniper.Framework.Cache;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace AutoSniper.ClientWPF.Repositories
{
    public static class CurrencyRepository
    {
        public static IEnumerable<Currency> GetAllCurrency()
        {
            var cache = RuntimeCache.BuildCache();
            var data = cache.Get<IEnumerable<Currency>>("CurrencyKey");
            if (data != null && data.Any()) { return data; }
            var sql = "SELECT * FROM Currency WHERE CurrencyId > 0";
            data = DataProvider.GetConnection().Query<Currency>(sql);
            cache.Put("CurrencyKey", data);
            return data;
        }
        public static Currency GetCurrency(string name)
        {
            return GetAllCurrency().FirstOrDefault(s => s.Name == name);
        }
    }
}
