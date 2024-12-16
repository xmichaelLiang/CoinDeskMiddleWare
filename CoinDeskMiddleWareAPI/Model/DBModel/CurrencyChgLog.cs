using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyDBContext.Models
{
    public class CurrencyChgLog
    {
        public string LogID { get; set; }

        public string OldData { get; set; }
        public string NewData { get; set; }
        public string Operation { get; set; }
        public string ModifyUser { get; set; }

        public DateTime ModifyDate { get; set; }
    }
}