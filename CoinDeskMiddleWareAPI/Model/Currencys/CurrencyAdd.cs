using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.Currencys
{
    public class CurrencyAdd
    {
        /// <summary>
        /// 幣別碼，例如 USD, EUR 等。
        /// </summary>
        [Required]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// 幣別名稱，例如美元, 歐元 等。
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 執行操作的使用者 ID。
        /// </summary>
        public string UserID { get; set; }
    }
}