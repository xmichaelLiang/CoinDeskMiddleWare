using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.CoinDesk.BPI
{
    public class BpiModel
    {
         public string Code { get; set; }
        public string Symbol { get; set; }
        public string Rate { get; set; }
        public string Description { get; set; }
        public float Rate_float { get; set; }
    }
}