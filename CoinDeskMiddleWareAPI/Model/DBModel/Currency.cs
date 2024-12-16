using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyDBContext.Models
{
    public class Currency
    {
       
        public int CurrencyId  { get; set; }
        public string CurrencyCode { get; set; }
        public string Name { get; set; }
        public string CreateID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; } 
    }
}
