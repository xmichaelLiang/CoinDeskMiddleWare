# CoinDeskMiddleWare
## 如何運行專案
### 環境需求
### Visual Studio 2022
- 直接用Visual Studio 2022 執行偵錯即可。
  
### Docker
- Docker & Docker Compose
- .NET 8.0 SDK
- 到專案目錄CoinDeskMiddleWare.sln 的該層，有Docker File與yam檔
- 於該層在控制台執行以下指令：
 ```bash
docker-compose up --build
```
- 目前用Windows 上安裝Docker Desktop 可正確執行。

## 資料庫建置
### 使用Migrations
- 於CoinDeskMiddleWareAPI 專案中有建構設定產生資料庫、資料表的Migrations，專案啟動時會自動建立DB與Table
- 於Windows 執行時，請確定有安裝 Visual Studio 2022的「Data storage and processing (資料儲存與處理)」的項目
- 或是下載 SQL Server Express 安裝程式

### 不使用Migrations
- 請在appsettings.json中將MigrationsDB設定為N
- 建立DB時，為方便測試請將DB命名為CoinDeskDb
- 不使用Migration時，請在建立完DB後，執行以下SQL

## Create Table 的SQL
### Currency、CurrencyChgLog兩個Table都必需建立。
- Currency 儲存幣別代碼與中文名稱
```sql
CREATE TABLE Currency (
    CurrencyId INT IDENTITY(1,1) PRIMARY KEY, -- 主鍵，採用自動增加
    CurrencyCode NVARCHAR(50) NOT NULL,      -- 不得為空
    Name NVARCHAR(100) NOT NULL,            -- 不得為空
    CreateID NVARCHAR(50) NOT NULL,         -- 可根據需求設定是否為 NULL
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(), -- 預設為當前時間
    UpdatedAt DATETIME NULL,                -- 可為空
    UpdatedBy NVARCHAR(50) NULL             -- 可為空
);

-- 為 CurrencyId 建立索引（自動為主鍵建立索引，以下為額外的冗餘示範）
CREATE INDEX IDX_CurrencyId ON Currency (CurrencyId);

-- 為 CurrencyCode 建立索引
CREATE UNIQUE INDEX UQ_CurrencyCode ON Currency (CurrencyCode);
```
- CurrencyChgLog LogCurrency的修改紀錄
```sql
 CREATE TABLE CurrencyChgLog (
    LogID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), -- 主鍵，使用 UUID 並設置預設值
    OldData NVARCHAR(MAX) NULL,                        -- 可為空，記錄舊資料
    NewData NVARCHAR(MAX) NULL,                        -- 可為空，記錄新資料
    Operation NVARCHAR(50) NOT NULL,                   -- 操作類型（如 INSERT, UPDATE, DELETE）
    ModifyUser NVARCHAR(50) NOT NULL,                  -- 修改使用者
    ModifyDate DATETIME NOT NULL DEFAULT GETDATE()     -- 修改日期，預設為當前時間
);
```

## 測試API與Token
### 注意事項
- 可使用Swagger、POST MAN測試
- API 使用JWT Token認證，在Header中要帶入Bearer Token
   - POST MAN 在Authorization 選擇Bearer Token
   - Swagger 右上角的Authorize，點選進入後，貼上測試Token，再點擊Authorize。
- 測試Token
```bash
v7gt5kVXTrOPSjgql/3Hyw3MzFYYHJbQstyoWfzlg3MZNFZiUNJjqZyOkhJ3755280drNNGH7ijV3sRWcbktRd5dx/fhIAsAYptlY/BF6LXKL9mhGiJqcxE1gPt1NPIaJjHNdEM8/JRNn4qsIUyXijGf310i9J+xR0PKt9snCu4vAG5BC10l2PB7PGfvNRMcAvGJdvlWpcJ2q/TlOqQ+WvrMkzE8MIg//a1wTLUDcLTaYfE+3npKrSvXiffg+Rn3QbQzpSDn8qN24IvUqgi4i8DLX1aqU62prpLFvz5iaYO6gFR658wwI2tncqDYsYY+5LgkBViWMXtGsYK8LCAS+g==
```
## 實作加分題
### 印出所有 API 被呼叫以及呼叫外部 API 的 request and response body log
### 新增幣別 /api/Currency/CurrNew001
  - Request
 ```json
{
  "currencyCode": "GBP",
  "name": "英鎊",
  "userID": "A001"
}
```
  - Response
```json
{
  "code": "200",
  "message": "新增成功",
  "data": null
}
```

### 查詢幣別 /api/Currency/CurrQ001
- Request
```json
``` 
- Response
```json
{
  "code": "200",
  "message": "OK",
  "data": [
    {
      "currencyId": 5,
      "currencyCode": "EUR",
      "name": "歐元"
    },
    {
      "currencyId": 3,
      "currencyCode": "GBP",
      "name": "英鎊"
    },
    {
      "currencyId": 4,
      "currencyCode": "USD",
      "name": "美元"
    }
  ]
}
```

### 修改幣別 /api/Currency/CurrU001
- Request
```json
{
  "currencyId": 4,
  "currencyCode": "USD",
  "name": "美金",
  "userID": "A001"
}
``` 
- Response
```json
{
  "code": "200",
  "message": "更新成功",
  "data": null
}
```

### 刪除幣別 /api/Currency/CurrD001
- Request
```json
{
  "currencyId": 4,
  "currencyCode": "USD"
  "userID": "A001"
}
```
- Response
```json
{
  "code": "200",
  "message": "刪除成功",
  "data": null
}
```

### 查詢BPI /api/CoinDesk/BpiQ001
- Request
```json
```
- Response
```json
{
  "code": "200",
  "message": "查詢成功!",
  "data": [
    {
      "currencyCode": "EUR",
      "name": "歐元",
      "rate": 101300.51,
      "updatedAt": "2024/12/17 14:37:00"
    },
    {
      "currencyCode": "GBP",
      "name": "英鎊",
      "rate": 83476.29,
      "updatedAt": "2024/12/17 14:37:00"
    }
  ]
}
```
### 查詢https://api.coindesk.com/v1/bpi/currentprice.json
- Request
```json
```
- Response
```json
{
"time":{"updated":"Dec 17, 2024 06:41:36 UTC","updatedISO":"2024-12-17T06:41:36+00:00","updateduk":"Dec 17, 2024 at 06:41 GMT"},
"disclaimer":"This data was produced from the CoinDesk Bitcoin Price Index (USD). Non-USD currency data converted using hourly conversion rate from openexchangerates.org",
"chartName":"Bitcoin",
"bpi":{
"USD":{"code":"USD","symbol":"&#36;","rate":"106,611.903","description":"United States Dollar","rate_float":106611.9028},
"GBP":{"code":"GBP","symbol":"&pound;","rate":"83,420.935","description":"British Pound Sterling","rate_float":83420.9354},
"EUR":{"code":"EUR","symbol":"&euro;","rate":"101,233.332","description":"Euro","rate_float":101233.3323}
}
}
```

### Error handling 處理 API response
- 使用MiddleWare 統一處理 系統擲出的500錯誤，在程式CoinDeskMiddleWareAPI\MiddlerWare\ErrorHandlingMiddleWare.cs
- 在Program.cs使用app.UseMiddleware<ErrorHandlingMiddleWare>();註冊MiddleWare處理系統擲出的500錯誤，同一回傳JOSN

### swagger-ui
- 有使用swagger-ui，請注意需在環境是Docker或Development才會顯示
- https://host/swagger/index.html

### 多語系設計
- 有使用多語系zh-TW、en-US，可在header 中帶入Accept-Language 指定語系
- 語系檔放在CoinDeskMiddleWareAPI\Repository 下
- 多語系設定目前針對API Response 的message做設定，所以CurrencyDataService.cs、DefaultCoinDeskBPIQueryStrategy.cs都有DI IStringLocalizerFactory，並使用IStringLocalizer帶設定值的內容。

### design pattern 實作
- 有使用策略模式。
- 考量有可能會需要查詢特定幣種的比特比值，可用同一個API 接口呼叫 /api/CoinDesk/BpiQ001，查詢單一幣別時，/api/CoinDesk/BpiQ001/{CurrencyCode}，用策略模式依照是否有傳入幣別，分別執行查詢全部與單一幣種的邏輯。
- CoinDeskMiddleWareAPI\Service\Strategies 下 使用ICoinDeskBPIQueryStrategy.cs 分別實作給DefaultCoinDeskBPIQueryStrategy.cs和SpecificCurrencyCoinDeskStrategy.cs ，進行不同的查詢策略。
- 在Controller中透過工廠ICoinDeskBPIQueryStrategyFactory.cs、CoinDeskBPIQueryStrategyFactory.cs回傳對應的策略。
 ```csharp
// File: ICoinDeskBPIQueryStrategy.cs
public interface ICoinDeskBPIQueryStrategy
{
     Task<apiResultModel> GetCurrencyData(string currency);
}
```

 ```csharp
// File: DefaultCoinDeskBPIQueryStrategy.cs
public class DefaultCoinDeskBPIQueryStrategy : ICoinDeskBPIQueryStrategy
{
  
    public DefaultCoinDeskBPIQueryStrategy()
    {
      ......
    }

    public async Task<apiResultModel> GetCurrencyData(string currency)
    {
     ......
    }
}
```
 ```csharp
// File:  SpecificCurrencyCoinDeskStrategy.cs
public class SpecificCurrencyCoinDeskStrategy : ICoinDeskBPIQueryStrategy
{
  
    public SpecificCurrencyCoinDeskStrategy()
    {
      ......
    }

    public async Task<apiResultModel> GetCurrencyData(string currency)
    {
     ......
    }
}
```
 ```csharp
// File:   ICoinDeskBPIQueryStrategyFactory.cs
 public interface ICoinDeskBPIQueryStrategyFactory
 {
     ICoinDeskBPIQueryStrategy GetStrategy(string currency);
 }
```
 ```csharp
// File:   CoinDeskBPIQueryStrategyFactory.cs
 private readonly IServiceProvider _serviceProvider;

 public CoinDeskBPIQueryStrategyFactory(IServiceProvider serviceProvider)
 {
     _serviceProvider = serviceProvider;
 }

 public  ICoinDeskBPIQueryStrategy GetStrategy(string currency)
 {
     if (currency == "ALL")
     {
         return _serviceProvider.GetService<DefaultCoinDeskBPIQueryStrategy>();
     }
     else
     {
         return _serviceProvider.GetService<SpecificCurrencyCoinDeskStrategy>();
     }
 }
```
 ```csharp
// File:   ICoinDeskBPIQueryStrategyFactory.cs
 public interface ICoinDeskBPIQueryStrategyFactory
 {
     ICoinDeskBPIQueryStrategy GetStrategy(string currency);
 }
```
 ```csharp
// File:   CoinDeskController.cs
       [HttpPost("BpiQ001/{currencyCode}")]
      [HttpPost("BpiQ001")]
      public async Task<IActionResult> QueryBpi(string currencyCode = "ALL")
      {
          var strategy = _strategyFactory.GetStrategy(currencyCode);
          apiResultModel data = await strategy.GetCurrencyData(currencyCode);
          return Ok(data);
      }
```
### 能夠運行在 Docker
- 有測試過用Windows 上安裝Docker Desktop 可正確執行
- Docker File與yam在CoinDeskMiddleWare.sln同一層folder中docker-compose.yml和Dockerfile

### 加解密技術應用 (AES/RSA…etc.)
- Token核發後，有使用AES加密後才會給Client端。
- AES加密的Service在Utility\EncryptUtility\AESUtility.cs
- 所以驗證Token時，會先解密後才進行驗證。在Program的76~103行。
 ```csharp
// File:   Program.cs
.....
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var encryptedToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var decryptedToken =AesEncryptionService.Decrypt(encryptedToken, aesKey);
            context.Token = decryptedToken;
            return Task.CompletedTask;
        }
    };
});
.....
```



  
