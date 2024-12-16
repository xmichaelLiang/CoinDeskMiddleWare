using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Service.Strategies;

namespace CoinDeskMiddleWareAPI.Service.Factorys
{
  
    public class CoinDeskBPIQueryStrategyFactory: ICoinDeskBPIQueryStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CoinDeskBPIQueryStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public  ICoinDeskBPIQueryStrategy GetStrategy(string currency)
        {
            if (currency == "ALL")
            {
                return _serviceProvider.GetService<DefaultCoinDeskBPIQueryStrategy>();
            }
            else
            {
                return _serviceProvider.GetService<SpecificCurrencyCoinDeskStrategy>();
            }
        }
    }
}