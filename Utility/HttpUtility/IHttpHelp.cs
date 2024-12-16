using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Models;

namespace Utility.HttpUtility
{
    public interface IHttpHelp
    {
        Task<string> GetRestAPI(string url, List<HeaderPara> headerParas, int reTryTimes = 0);
    }
}