using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.Currencys
{
    public class CurrencyQueryResult
    {
        /// <summary>
        /// 幣別唯一識別Id。
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// 幣別名稱，例如美元, 歐元 等。
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// 執行操作的使用者 ID。
        /// </summary>
        public string Name { get; set; }
    }
}