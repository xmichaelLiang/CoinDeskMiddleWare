using CoinDeskMiddleWareAPI.Model.Currencys;
using CoinDeskMiddleWareAPI.Model;
using Microsoft.AspNetCore.Mvc;
using CoinDeskMiddleWareAPI.Service.Currencys;
using CoinDeskMiddleWareAPI.Controllers;
using Moq;

namespace CoinDeskMiddleWareAPI.Tests;

public class CurrencyControllerTests
{
     private readonly Mock<ICurrencyDataService> _mockCurrencyDataService;
    private readonly CurrencyController _controller;

    public CurrencyControllerTests()
    {
        _mockCurrencyDataService = new Mock<ICurrencyDataService>();
        _controller = new CurrencyController(_mockCurrencyDataService.Object);
    }

    [Fact]
    public async Task QueryCurrency_ReturnsOkResult()
    {
        // Arrange
        var currencyCode = "";
        var results = new List<CurrencyQueryResult> { 
            new CurrencyQueryResult { CurrencyCode = "USD",CurrencyId=1,Name="美元" } ,
            new CurrencyQueryResult { CurrencyCode = "GBK",CurrencyId=2,Name="英鎊" },
             new CurrencyQueryResult { CurrencyCode = "EUR",CurrencyId=3,Name="歐元" }
        };
        _mockCurrencyDataService.Setup(service => service.QueryCurrency(currencyCode))
            .ReturnsAsync(results);

        // Act
        var result = await _controller.QueryCurrency();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var apiResult = Assert.IsType<apiResultModel>(okResult.Value);
        Assert.Equal("200", apiResult.code);
        Assert.Equal("OK", apiResult.message);
        Assert.Equal(results, apiResult.data);
    }

    [Fact]
    public async Task UpdCurrency_ReturnsOkResult_WhenUpdateIsSuccessful()
    {
        // Arrange
        var currencyUpd = new CurrencyUpd { CurrencyId = 1, CurrencyCode = "USD", Name = "美金",UserID="A001" };
        var apiResult = new apiResultModel { code = "200", message = "Update successful" };
        _mockCurrencyDataService.Setup(service => service.UpdCurrency(currencyUpd))
            .ReturnsAsync(apiResult);

        // Act
        var result = await _controller.UpdCurrency(currencyUpd);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedApiResult = Assert.IsType<apiResultModel>(okResult.Value);
        Assert.Equal("200", returnedApiResult.code);
        Assert.Equal("Update successful", returnedApiResult.message);
    }

    [Fact]
    public async Task AddCurrency_ReturnsOkResult_WhenAddIsSuccessful()
    {
        // Arrange
        var currencyAdd = new CurrencyAdd { CurrencyCode = "TWD", Name = "新台幣",UserID="A001" };
        var apiResult = new apiResultModel { code = "200", message = "Add successful" };
        _mockCurrencyDataService.Setup(service => service.AddCurrency(currencyAdd))
            .ReturnsAsync(apiResult);

        // Act
        var result = await _controller.AddCurrency(currencyAdd);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedApiResult = Assert.IsType<apiResultModel>(okResult.Value);
        Assert.Equal("200", returnedApiResult.code);
        Assert.Equal("Add successful", returnedApiResult.message);
    }

    [Fact]
    public async Task DelCurrency_ReturnsOkResult_WhenDeleteIsSuccessful()
    {
        // Arrange
        var currencyDel = new CurrencyDel { CurrencyId = 1,UserID="A001" };
        var apiResult = new apiResultModel { code = "200", message = "Delete successful" };
        _mockCurrencyDataService.Setup(service => service.DelCurrency(currencyDel))
            .ReturnsAsync(apiResult);

        // Act
        var result = await _controller.DelCurrency(currencyDel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedApiResult = Assert.IsType<apiResultModel>(okResult.Value);
        Assert.Equal("200", returnedApiResult.code);
        Assert.Equal("Delete successful", returnedApiResult.message);
    }
}