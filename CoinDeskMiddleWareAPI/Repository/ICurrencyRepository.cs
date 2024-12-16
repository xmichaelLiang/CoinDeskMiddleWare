using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model.Currencys;
using CurrencyDBContext.Models;

namespace CoinDeskMiddleWareAPI.Repository
{
    public interface ICurrencyRepository
    {
         Task<List<CurrencyQueryResult>> QueryCurrency(string currencyCode="");

         Task<Currency> QueryCurrency(int currencyid);
         Task<List<CurrencyQueryResult>> QueryCurrency(List<string> currencyCodes);
         Task UpdCurrency(CurrencyUpd currencyUpd);
         Task<bool>  DelCurrency(int CurrencyId);
         Task AddCurrency(Currency currency);
    }
}