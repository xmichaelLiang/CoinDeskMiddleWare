using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model
{
    public class apiResultModel
    {
        public string code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}