using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Repository
{
    public interface ICurrencyChgLogsRepository
    {
        Task AddCurrencyChgLog(string oldData, string newData, string operation, string modifyUser);
    }
}