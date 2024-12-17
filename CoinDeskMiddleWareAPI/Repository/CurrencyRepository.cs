using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model.Currencys;
using CurrencyDBContext;
using CurrencyDBContext.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CoinDeskMiddleWareAPI.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyDbContext _currencyDbContext;
        public CurrencyRepository(CurrencyDbContext currencyDbContext)
        {
            _currencyDbContext = currencyDbContext;
        }

        public async Task AddCurrency(Currency currency)
        {
               _currencyDbContext.Currencies.Add(currency);
               await  _currencyDbContext.SaveChangesAsync();
        }

        public async Task<bool> DelCurrency(int CurrencyId)
        {
            var currency = await  _currencyDbContext.Currencies.FindAsync(CurrencyId);
             if(currency == null)
                 return false;
             _currencyDbContext.Currencies.Remove(currency);
             await  _currencyDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<CurrencyQueryResult>> QueryCurrency(string currencyCode = "")
        {
            List < CurrencyQueryResult > result = new List<CurrencyQueryResult> ();

            if (string.IsNullOrEmpty(currencyCode)) {
                result=await _currencyDbContext.Currencies
                .Select(c => new CurrencyQueryResult
                {
                    CurrencyCode = c.CurrencyCode,
                    Name = c.Name,
                    CurrencyId = c.CurrencyId
                }).OrderBy(c => c.CurrencyCode)
                .ToListAsync();
                return result;
            }

            return await _currencyDbContext.Currencies
                .Where(c=>c.CurrencyCode == currencyCode)
                .Select(c => new CurrencyQueryResult
                {
                    CurrencyCode = c.CurrencyCode,
                    Name = c.Name,
                    CurrencyId = c.CurrencyId
                })
                .ToListAsync();
        }

        public async Task<List<CurrencyQueryResult>> QueryCurrency(List<string> currencyCodes)
        {
            return await _currencyDbContext.Currencies
                .Where(c => currencyCodes.Contains(c.CurrencyCode))
                .Select(c => new CurrencyQueryResult
                {
                    CurrencyCode = c.CurrencyCode,
                    Name = c.Name,
                    CurrencyId =c.CurrencyId
                })
                .OrderBy(c => c.CurrencyCode)
                .ToListAsync();
        }

        public async Task<Currency> QueryCurrency(int currencyid)
        {
               return await _currencyDbContext.Currencies
                .Where(c => c.CurrencyId == currencyid)
                .FirstOrDefaultAsync();
        }

        public async Task<Currency> QueryCurrency(int currencyid, string currencyCode)
        {
            return await _currencyDbContext.Currencies
             .Where(c => c.CurrencyId == currencyid && c.CurrencyCode == currencyCode)
             .FirstOrDefaultAsync();
        }

        public async Task UpdCurrency(CurrencyUpd currencyUpd)
        {
            var currency = await _currencyDbContext.Currencies
                .FirstOrDefaultAsync(c => c.CurrencyId == currencyUpd.CurrencyId);

            if (currency == null)
            {
                throw new KeyNotFoundException("Currency not found.");
            }

            if (!string.IsNullOrEmpty(currencyUpd.CurrencyCode))
                currency.CurrencyCode = currencyUpd.CurrencyCode;
           

            if (!string.IsNullOrEmpty(currencyUpd.Name))
                currency.Name = currencyUpd.Name;
         
            currency.UpdatedAt = DateTime.Now; 
            currency.UpdatedBy = currencyUpd.UserID;
            await _currencyDbContext.SaveChangesAsync();
        }

       
    }
}