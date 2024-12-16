using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.CoinDesk.BPI
{
    public class CurrencyBpiModel
    {
        public TimeModel Time { get; set; }
        public string Disclaimer { get; set; }
        public string ChartName { get; set; }
        public Dictionary<string, BpiModel> Bpi { get; set; }
    }
}