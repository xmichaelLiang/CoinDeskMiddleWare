using CoinDeskMiddleWareAPI.Service.Strategies;

namespace CoinDeskMiddleWareAPI.Service.Factorys
{
    public interface ICoinDeskBPIQueryStrategyFactory
    {
        ICoinDeskBPIQueryStrategy GetStrategy(string currency);
    }
}
