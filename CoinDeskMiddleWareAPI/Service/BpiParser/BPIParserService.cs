using CoinDeskMiddleWareAPI.Model.CoinDesk;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPI;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPIResponse;
using CoinDeskMiddleWareAPI.Model.Currencys;

namespace CoinDeskMiddleWareAPI.Service.BpiParser
{
    public class BPIParserService : IBPIParserService
    {
        public List<BPICurrencyModel> ParserBPIResult(Dictionary<string, BpiModel> bpis, List<CurrencyQueryResult> currencys, TimeModel timeModel)
        {
            List<BPICurrencyModel> bPICurrencyModels = new List<BPICurrencyModel>();
            foreach (var currency in currencys)
            {
                BpiModel bpiModel = bpis[currency.CurrencyCode];
                if (bpiModel == null)
                    continue;
                BPICurrencyModel bPICurrencyModel = new BPICurrencyModel();
                bPICurrencyModel.currencyCode = currency.CurrencyCode;
                bPICurrencyModel.name = currency.Name;
                bPICurrencyModel.rate = bpiModel.Rate_float;// Decimal.Parse(bpiModel.Rate);
                DateTime updAt = DateTime.Parse(timeModel.UpdatedISO);
                bPICurrencyModel.updatedAt = updAt.ToString("yyyy/MM/dd HH:mm:ss");
                bPICurrencyModels.Add(bPICurrencyModel);
            }
            return bPICurrencyModels;
        }
    }
}
