using CoinDeskMiddleWareAPI.Controllers;
using CoinDeskMiddleWareAPI.Model;
using CoinDeskMiddleWareAPI.Model.CoinDesk.BPIResponse;
using CoinDeskMiddleWareAPI.Service;
using CoinDeskMiddleWareAPI.Service.Factorys;
using CoinDeskMiddleWareAPI.Service.Strategies;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CoinDeskMiddleWareAPI.Tests
{
   public class CoinDeskControllerTests
    {
        private readonly Mock<ICoinDeskBPIQueryStrategyFactory> _mockStrategyFactory;
        private readonly Mock<ICoinDeskBPIQueryStrategy> _mockStrategy;
        private readonly CoinDeskController _controller;

        public CoinDeskControllerTests()
        {
            _mockStrategyFactory = new Mock<ICoinDeskBPIQueryStrategyFactory>();
            _mockStrategy = new Mock<ICoinDeskBPIQueryStrategy>();
            _controller = new CoinDeskController(_mockStrategyFactory.Object);
        }

          [Fact]
            public async Task QueryBpi_ReturnsOkResult_WhenCurrencyCodeIsAll()
            {
                // Arrange
                var currencyCode = "ALL";
                List<BPICurrencyModel> results = new List<BPICurrencyModel> { 
                    new BPICurrencyModel { currencyCode = "USD", name = "美元",rate=200.2F,updatedAt="2024-12-15 12:00:00" },
                    new BPICurrencyModel { currencyCode = "EUR", name = "歐元",rate=300.2F,updatedAt="2024-12-15 12:00:00" },
                    new BPICurrencyModel { currencyCode = "GBK", name = "英鎊",rate=100.2F,updatedAt="2024-12-15 12:00:00" }    
                };
                var expectedData = new apiResultModel { code = "200", message = "Success", data = results   };
                _mockStrategy.Setup(strategy => strategy.GetCurrencyData(currencyCode))
                    .ReturnsAsync(expectedData);
                _mockStrategyFactory.Setup(factory => factory.GetStrategy(currencyCode))
                    .Returns(_mockStrategy.Object);

                // Act
                var result = await _controller.QueryBpi(currencyCode);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnedData = Assert.IsType<apiResultModel>(okResult.Value);
                Assert.Equal(expectedData.code, returnedData.code);
                Assert.Equal(expectedData.message, returnedData.message);
                Assert.Equal(expectedData.data, returnedData.data);
            }
    }
}