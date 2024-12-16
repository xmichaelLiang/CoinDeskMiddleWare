using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPI;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPIResponse;
using CoinDeskMiddleWareAPI.Model.Currencys;
using CoinDeskMiddleWareAPI.Service.BpiParser;
using CoinDeskMiddleWareAPI.Service.Currencys;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Utility.HttpUtility;

namespace CoinDeskMiddleWareAPI.Service.Strategies
{
    public class DefaultCoinDeskBPIQueryStrategy : ICoinDeskBPIQueryStrategy
    {
       private readonly IHttpHelp  _HttpHelp;
        private readonly IConfiguration _Configuration;
        private readonly ICurrencyDataService _currencyDataService;
        private readonly IBPIParserService _iBPIParserService;
           private readonly IStringLocalizer _localizer;
        public DefaultCoinDeskBPIQueryStrategy(IHttpHelp httpHelp, IConfiguration configuration, ICurrencyDataService currencyDataService, IBPIParserService iBPIParserService, IStringLocalizerFactory localizerFactory)
        {
            _HttpHelp=httpHelp;
            _Configuration =configuration;
            _currencyDataService = currencyDataService;
            _iBPIParserService = iBPIParserService;
            _localizer = localizerFactory.Create("Localization", typeof(Program).Assembly.GetName().Name);
        }
       
       
        public async Task<apiResultModel> GetCurrencyData(string currency)
        {
            CurrencyBpiModel currencyBpiModel = await GetCurrencyFormBpiAPI();
            List<string> CurrencyCodes = currencyBpiModel.Bpi.Keys.ToList();
            List<CurrencyQueryResult> currencyResult = await _currencyDataService.QueryCurrency(CurrencyCodes);
            List<BPICurrencyModel> bPICurrencyModels = _iBPIParserService.ParserBPIResult(currencyBpiModel.Bpi, currencyResult, currencyBpiModel.Time);
            apiResultModel apiResultModel = new apiResultModel();
            apiResultModel.code = "200";
            apiResultModel.message = _localizer["GetCurrencyDataSuccess"];
            apiResultModel.data = bPICurrencyModels;
            return apiResultModel;
        }

        private async Task<CurrencyBpiModel> GetCurrencyFormBpiAPI() {
            string url = _Configuration["CoinsDeskURL:BPI"];
            string responseBodyString =await _HttpHelp.GetRestAPI(url, new List<Utility.Models.HeaderPara>(), 2);
            CurrencyBpiModel currencyBpiModel= JsonConvert.DeserializeObject<CurrencyBpiModel>(responseBodyString);
            return currencyBpiModel;
        }
    }

}