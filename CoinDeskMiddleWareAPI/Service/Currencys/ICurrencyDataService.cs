using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Model.Currencys;

namespace CoinDeskMiddleWareAPI.Service.Currencys
{
    public interface ICurrencyDataService
    {
         Task<List<CurrencyQueryResult>> QueryCurrency(string currencyCode="");
         Task<List<CurrencyQueryResult>> QueryCurrency(List<string> currencyCodes);
         Task<apiResultModel> UpdCurrency(CurrencyUpd currencyUpd);
         Task<apiResultModel>  DelCurrency(CurrencyDel currencyDel);
         Task<apiResultModel> AddCurrency(CurrencyAdd  currencyAdd);
    }
}