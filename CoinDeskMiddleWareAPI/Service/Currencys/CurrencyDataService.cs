using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Model.Currencys;
using CoinDeskMiddleWareAPI.Repository;
using CurrencyDBContext.Models;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace CoinDeskMiddleWareAPI.Service.Currencys
{
    public class CurrencyDataService:ICurrencyDataService
    {
        private readonly  ICurrencyRepository _currencyRepository;
         private readonly  ICurrencyChgLogsRepository _currencyChgLogsRepository;
        private readonly IStringLocalizer _localizer;

        public CurrencyDataService( ICurrencyRepository currencyRepository, ICurrencyChgLogsRepository currencyChgLogsRepository, IStringLocalizerFactory localizerFactory)
        {
            _currencyRepository=currencyRepository;
            _currencyChgLogsRepository=currencyChgLogsRepository;
             _localizer = localizerFactory.Create("Localization", typeof(Program).Assembly.GetName().Name) ;
        }

         public async Task<List<CurrencyQueryResult>> QueryCurrency(string currencyCode=""){
                return await _currencyRepository.QueryCurrency(currencyCode);
        }
         public async Task<List<CurrencyQueryResult>> QueryCurrency(List<string> currencyCodes){
                return await _currencyRepository.QueryCurrency(currencyCodes);
         }

         public async Task<apiResultModel> UpdCurrency(CurrencyUpd currencyUpd){
            apiResultModel apiResultModel = new apiResultModel();
            apiResultModel.code="200";
            apiResultModel.message= _localizer["UpdCurrencySuccess"];
            List<CurrencyQueryResult> currencyQueryResults = await _currencyRepository.QueryCurrency(currencyUpd.CurrencyCode);
               if(currencyQueryResults.Count>0){
                  var isExistCurrency = currencyQueryResults.Where(x=>x.CurrencyId!=currencyUpd.CurrencyId).FirstOrDefault();
                   if(isExistCurrency !=null)
                          return ProcessResult("409", _localizer["CurrencyCodeExistsWithId", currencyUpd.CurrencyCode, currencyUpd.CurrencyId]);
               }

             Currency currency = await _currencyRepository.QueryCurrency(currencyUpd.CurrencyId);

            if(currency==null)
                 return ProcessResult("440",_localizer["CurrencyIDNotFound"]);
            string currencyJson = JsonConvert.SerializeObject(currency);
            string NewCurrency = JsonConvert.SerializeObject(currencyUpd);
            await AddCurrencyChgLog(currencyJson,NewCurrency,"Update",currencyUpd.UserID);
            currency.CurrencyCode=currencyUpd.CurrencyCode;
          
             await _currencyRepository.UpdCurrency(currencyUpd);
             return apiResultModel;
         }

        private apiResultModel ProcessResult(string code, string message,object data=null)
        {
            apiResultModel apiResultModel = new apiResultModel();
             apiResultModel.code=code;
             apiResultModel.message=message;
             apiResultModel.data=data;
             return apiResultModel;
        }

        public async Task<apiResultModel> DelCurrency(CurrencyDel currencyDel){
                 apiResultModel apiResultModel = new apiResultModel();
                 apiResultModel.code="200";
                 apiResultModel.message=_localizer["DelCurrencySuccess"];
                Currency currency=   await _currencyRepository.QueryCurrency(currencyDel.CurrencyId, currencyDel.CurrencyCode);
                if(currency==null)
                    return ProcessResult("440",_localizer["CurrencyIDNotFound"]);

                string currencyJson = JsonConvert.SerializeObject(currency);
                await AddCurrencyChgLog(currencyJson,"","Delete",currencyDel.UserID);
                 await _currencyRepository.DelCurrency(currencyDel.CurrencyId);
                 return apiResultModel;
         }

         public async Task<apiResultModel> AddCurrency(CurrencyAdd currencyAdd){
            apiResultModel apiResultModel = new apiResultModel();
            apiResultModel.code="200";
            apiResultModel.message=_localizer["AddCurrencySuccess"];

               List<CurrencyQueryResult> currencyQueryResults = await _currencyRepository.QueryCurrency(currencyAdd.CurrencyCode);
               if(currencyQueryResults.Count>0)
                  return ProcessResult("409",_localizer["CurrencyCodeExists"]);

              Currency currency = new Currency();
              currency.CurrencyCode= currencyAdd.CurrencyCode;
              currency.Name= currencyAdd.Name;
              currency.CreateID = currencyAdd.UserID;
              currency.CreatedAt = DateTime.Now;
              string currencyJson = JsonConvert.SerializeObject(currency);
             await  AddCurrencyChgLog("",currencyJson,"Insert",currencyAdd.UserID);
             await _currencyRepository.AddCurrency(currency);
             return apiResultModel;
         }

         private async Task AddCurrencyChgLog(string oldData, string newData, string operation, string modifyUser)
         {
            await  _currencyChgLogsRepository.AddCurrencyChgLog(oldData,newData,operation,modifyUser);
         }
    }
}