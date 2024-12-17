using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.Model.Currencys
{
    public class  CurrencyDel
    {

        /// <summary>
        /// ���O�ߤ@�ѧOId�C
        /// </summary>
        [Required]
        public int CurrencyId { get; set; }

        /// <summary>
        /// ���O�X�A�Ҧp USD, EUR ���C
        /// </summary>
        [Required]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// ����ާ@���ϥΪ� ID�C
        /// </summary>
        public string UserID { get; set; }
    }
}