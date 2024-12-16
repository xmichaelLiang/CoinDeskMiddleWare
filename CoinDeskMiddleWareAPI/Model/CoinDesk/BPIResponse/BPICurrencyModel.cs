using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.CoinDesk.BPIResponse
{
    public class BPICurrencyModel
    {
        public string currencyCode {get;set;}

        public string name {get;set;}

        public float rate {get;set;}

        public string updatedAt {get;set;}


    }
}