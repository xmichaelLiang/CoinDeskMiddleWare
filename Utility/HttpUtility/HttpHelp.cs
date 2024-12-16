using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Models;

namespace Utility.HttpUtility
{
    public class HttpHelp : IHttpHelp
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpHelp(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetRestAPI(string url, List<HeaderPara> headerParas, int reTryTimes = 0)
        {
            var _client = _httpClientFactory.CreateClient();
            foreach (HeaderPara headerPara in headerParas)
                _client.DefaultRequestHeaders.Add(headerPara.Key, headerPara.Value);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12;

            int attempt = 0;
            do
            {
                try
                {
                    var response = await _client.GetAsync(url);
                    string resultString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return resultString;
                    }

                    throw new HttpRequestException($"呼叫API{url} 失敗，Status Code:{response.StatusCode}:{resultString}");
                }
                catch (HttpRequestException ex)
                {
                    attempt++;
                    if (attempt > reTryTimes)
                    {

                        throw new Exception($"在嘗試 {attempt} 次後失敗: {ex.Message}", ex);
                    }


                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)));
                }
            } while (attempt <= reTryTimes);

            throw new Exception("發生無法預期的錯誤。");
        }
    }
}
