using CoinDeskMiddleWareAPI.Model.CoinDesk;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPI;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPIResponse;
using CoinDeskMiddleWareAPI.Model.Currencys;

namespace CoinDeskMiddleWareAPI.Service.BpiParser
{
    public interface IBPIParserService
    {
        List<BPICurrencyModel> ParserBPIResult(Dictionary<string, BpiModel> bpis, List<CurrencyQueryResult> currencys, TimeModel timeModel);

    }
}
