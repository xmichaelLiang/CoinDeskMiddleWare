using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Service.Factorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinDeskMiddleWareAPI.Controllers
{
    [Authorize()]
    [ApiController]
    [Route("api/[controller]")]
    public class CoinDeskController : ControllerBase
    {
        private readonly ICoinDeskBPIQueryStrategyFactory _strategyFactory;
        public CoinDeskController(ICoinDeskBPIQueryStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }
        /// <summary>
        /// 查詢CoinDesk BPI資訊
        /// </summary>
        /// <returns> 查詢結果</returns>
        /// <response code="200">查詢成功</response>
        /// <response code="500">內部伺服器錯誤</response>
        [ProducesResponseType(typeof(apiResultModel), 200)]
        [ProducesResponseType(typeof(apiResultModel), 500)]
        [HttpPost("BpiQ001/{currencyCode}")]
        [HttpPost("BpiQ001")]
        public async Task<IActionResult> QueryBpi(string currencyCode = "ALL")
        {
            var strategy = _strategyFactory.GetStrategy(currencyCode);
            apiResultModel data = await strategy.GetCurrencyData(currencyCode);
            return Ok(data);
        }

   

    }
}