using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Utility.EncryptUtility;

namespace Utility.TokenUtility
{
    public class TokenHelp:ITokenHelp
    {
       private readonly IConfiguration _configuration;
       public TokenHelp(IConfiguration configuration){
              _configuration=configuration;
       }
       
        public string GenerateToken()
        {
                var tokenHandler = new JwtSecurityTokenHandler();
               
               var key = Encoding.UTF8.GetBytes( _configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                     Expires = DateTime.Now.AddYears(1),//測試練習用預設一年，應該要依照實際狀況調整
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenString= tokenHandler.WriteToken(token);
                 string EncryptToken= AesEncryptionService.Encrypt(tokenString, Convert.FromBase64String(_configuration["AES:Key"]));
                 return EncryptToken;
        }
    }
  
}