using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.Currencys
{
    public class CurrencyQueryResult
    {
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }

        public string Name { get; set; }
    }
}