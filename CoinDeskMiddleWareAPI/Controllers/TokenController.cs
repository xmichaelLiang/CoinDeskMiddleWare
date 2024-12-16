using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Utility.TokenUtility;

namespace CoinDeskMiddleWareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
       private readonly ITokenHelp _tokenHelp;
        public TokenController(ITokenHelp tokenHelp)
        {
            _tokenHelp = tokenHelp;
        }
         [HttpGet("GetToken")]
        public IActionResult GetToken()
        {
            string token=  _tokenHelp.GenerateToken();
            return Ok(token);
        }
    }
}