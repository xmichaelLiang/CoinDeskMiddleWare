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
            foreach (var bpi in bpis)
            {
                 string bpiCurrencyCode=bpi.Key;
                string currencyName = currencys.Where(c=>c.CurrencyCode == bpiCurrencyCode).Select(c=>c.Name).FirstOrDefault();

                if (string.IsNullOrEmpty(currencyName) == true)
                    currencyName = bpiCurrencyCode;
                
                BPICurrencyModel bPICurrencyModel = new BPICurrencyModel();
                bPICurrencyModel.currencyCode = bpiCurrencyCode;
                bPICurrencyModel.name = currencyName;
                bPICurrencyModel.rate = bpi.Value.Rate_float;
                DateTime updAt = DateTime.Parse(timeModel.UpdatedISO);
                bPICurrencyModel.updatedAt = updAt.ToString("yyyy/MM/dd HH:mm:ss");
                bPICurrencyModels.Add(bPICurrencyModel);
            }
            return bPICurrencyModels;
        }
    }
}
