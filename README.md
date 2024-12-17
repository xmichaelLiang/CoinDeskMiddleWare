# CoinDeskMiddleWare
## 如何運行專案
### 1. 環境需求
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
