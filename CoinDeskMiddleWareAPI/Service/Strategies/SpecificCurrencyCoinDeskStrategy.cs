using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPIResponse;

namespace CoinDeskMiddleWareAPI.Service.Strategies
{
    public class SpecificCurrencyCoinDeskStrategy : ICoinDeskBPIQueryStrategy
    {
        public async Task<apiResultModel> GetCurrencyData(string currency)
        {
           apiResultModel apiResultModel = new apiResultModel();
            apiResultModel.code="200";
            apiResultModel.message="您好，單一幣別查詢功能尚在建構中，敬請期待....";
            return apiResultModel;

        }
    }
}