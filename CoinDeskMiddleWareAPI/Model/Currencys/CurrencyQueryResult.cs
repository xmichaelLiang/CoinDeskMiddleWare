using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.Currencys
{
    public class CurrencyQueryResult
    {
        /// <summary>
        /// ���O�ߤ@�ѧOId�C
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// ���O�W�١A�Ҧp����, �ڤ� ���C
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// ����ާ@���ϥΪ� ID�C
        /// </summary>
        public string Name { get; set; }
    }
}