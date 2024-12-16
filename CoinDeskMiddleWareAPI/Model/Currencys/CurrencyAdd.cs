using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.Currencys
{
    public class CurrencyAdd
    {
        public string CurrencyCode { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
    }
}