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