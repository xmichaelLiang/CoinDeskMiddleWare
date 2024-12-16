using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyDBContext;
using CurrencyDBContext.Models;

namespace CoinDeskMiddleWareAPI.Repository
{
    public class CurrencyChgLogsRepository:ICurrencyChgLogsRepository
    {
        
        private readonly CurrencyDbContext _context;
        public CurrencyChgLogsRepository(CurrencyDbContext context)
        {
            _context=context;
        }
        public async Task AddCurrencyChgLog(string oldData, string newData, string operation, string modifyUser)
        {
              CurrencyChgLog currencyChgLog = new CurrencyChgLog();
              currencyChgLog.OldData=oldData;
                currencyChgLog.NewData=newData;
                currencyChgLog.Operation=operation;
                currencyChgLog.ModifyUser=modifyUser;
                currencyChgLog.ModifyDate=DateTime.Now;
               await _context.CurrencyChgLogs.AddAsync(currencyChgLog);
               await _context.SaveChangesAsync();
           
        }
    }
   
}