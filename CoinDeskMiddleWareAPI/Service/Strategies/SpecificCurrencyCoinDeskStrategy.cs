using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPIResponse;
using Microsoft.Extensions.Localization;

namespace CoinDeskMiddleWareAPI.Service.Strategies
{
    public class SpecificCurrencyCoinDeskStrategy : ICoinDeskBPIQueryStrategy
    {
       
        private readonly IStringLocalizer _localizer;

        public SpecificCurrencyCoinDeskStrategy(IStringLocalizerFactory localizerFactory)
        {
            _localizer =    _localizer = localizerFactory.Create("Localization", typeof(Program).Assembly.GetName().Name);
        }
        public async Task<apiResultModel> GetCurrencyData(string currency)
        {
           apiResultModel apiResultModel = new apiResultModel();
            apiResultModel.code="200";
            apiResultModel.message=_localizer["ComingSoon"];;
            return apiResultModel;

        }
    }
}