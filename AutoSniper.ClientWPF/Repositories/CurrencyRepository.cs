using AutoSniper.ClientWPF.Repositories.BaseProvider;
using AutoSniper.ClientWPF.Repositories.Models;
using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSniper.ClientWPF.Repositories
{
    public static class CurrencyRepository
    {
        public static IEnumerable<Currency> GetAllCurrency()
        {
            var sql = "SELECT * FROM Currency WHERE CurrencyId > 0";
            return DataProvider.GetConnection().Query<Currency>(sql);

            //eq相等 ne、neq不相等， gt大于， lt小于
            //var predicate = Predicates.Field<Currency>(s => s.CurrencyId, Operator.Gt, 0);
            //return DataProvider.GetConnection().GetList<Currency>();
        }

        //public static int UpdateCurrency(Currency currency)
        //{

        //}
    }
}
