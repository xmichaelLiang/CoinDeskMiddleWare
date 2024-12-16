using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPIResponse;

namespace CoinDeskMiddleWareAPI.Service.Strategies
{
    public interface ICoinDeskBPIQueryStrategy
    {
         Task<apiResultModel> GetCurrencyData(string currency);
    }
}