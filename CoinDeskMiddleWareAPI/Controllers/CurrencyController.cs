using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Model.Currencys;
using CoinDeskMiddleWareAPI.Service.Currencys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinDeskMiddleWareAPI.Controllers
{
    [Authorize()]
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyDataService _currencyDataService;
        public CurrencyController(ICurrencyDataService currencyDataService)
        {
            _currencyDataService = currencyDataService;
        }

        /// <summary>
        /// 查詢幣別設定資訊
        /// </summary>
        /// <returns> 查詢結果</returns>
        /// <response code="200">查詢成功</response>
        /// <response code="500">內部伺服器錯誤</response>
        [ProducesResponseType(typeof(apiResultModel), 200)]
        [ProducesResponseType(typeof(apiResultModel), 500)]
        [HttpPost("CurrQ001")]
        public async Task<IActionResult> QueryCurrency()
        {
            
         
           List<CurrencyQueryResult>   results=await _currencyDataService.QueryCurrency();
           apiResultModel apiResultModel = new apiResultModel();
            apiResultModel.code = "200";
            apiResultModel.message = "OK";
            apiResultModel.data = results;
            return Ok(apiResultModel);
        }

        /// <summary>
        /// 更新幣別資訊
        /// </summary>
        /// <param name="currencyUpd">幣別更新資料</param>
        /// <returns>更新結果</returns>
        /// <response code="200">更新成功</response>
        /// <response code="409">幣別代碼已存在</response>
        /// <response code="440">幣別 ID 與 幣別Code 組合未找到</response>
        /// <response code="500">內部伺服器錯誤</response>
          [HttpPost("CurrU001")]
          [ProducesResponseType(typeof(apiResultModel), 200)]
          [ProducesResponseType(typeof(apiResultModel), 409)]
          [ProducesResponseType(typeof(apiResultModel), 440)]
          [ProducesResponseType(typeof(apiResultModel), 500)]
        public async Task<IActionResult> UpdCurrency(CurrencyUpd currencyUpd)
        {

            apiResultModel apiResultModel1= await _currencyDataService.UpdCurrency(currencyUpd);
            if(apiResultModel1.code=="200")
                return Ok(apiResultModel1);

          return StatusCode(Int32.Parse(apiResultModel1.code), apiResultModel1);
        }

        /// <summary>
        /// 刪除幣別設定資訊
        /// </summary>
        /// <param name="currencydel">幣別刪除資料</param>
        /// <returns>刪除結果</returns>
        /// <response code="200">刪除成功</response>
        /// <response code="440">幣別 ID 與 幣別Code 組合未找到</response>  
        /// <response code="500">內部伺服器錯誤</response>
        [ProducesResponseType(typeof(apiResultModel), 200)]
        [ProducesResponseType(typeof(apiResultModel), 440)]
        [ProducesResponseType(typeof(apiResultModel), 500)]
        [HttpPost("CurrD001")]
        public async Task<IActionResult> DelCurrency( CurrencyDel  currencydel)
        {

            apiResultModel apiResultModel1= await _currencyDataService.DelCurrency(currencydel);
            if(apiResultModel1.code=="200")
                return Ok(apiResultModel1);

          return StatusCode(Int32.Parse(apiResultModel1.code), apiResultModel1);
        }

        /// <summary>
        /// 新增幣別設定資訊
        /// </summary>
        /// <param name="currencyAdd">幣別新增資料</param>
        /// <returns>新增結果</returns>
        /// <response code="200">新增成功</response>
        /// <response code="409">幣別代碼已存在</response>
        /// <response code="500">內部伺服器錯誤</response>
        [ProducesResponseType(typeof(apiResultModel), 200)]
        [ProducesResponseType(typeof(apiResultModel), 409)]
        [ProducesResponseType(typeof(apiResultModel), 500)]
        [HttpPost("CurrNew001")]
        public async Task<IActionResult> AddCurrency( CurrencyAdd  currencyAdd)
        {
            apiResultModel apiResultModel1= await _currencyDataService.AddCurrency(currencyAdd);

            if (apiResultModel1.code=="200")
                return Ok(apiResultModel1);

           return StatusCode(Int32.Parse(apiResultModel1.code), apiResultModel1);
        }
    }
}