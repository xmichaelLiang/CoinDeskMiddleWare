CREATE TABLE CurrencyChgLog (
    LogID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), -- 主鍵，使用 UUID 並設置預設值
    OldData NVARCHAR(MAX) NULL,                        -- 可為空，記錄舊資料
    NewData NVARCHAR(MAX) NULL,                        -- 可為空，記錄新資料
    Operation NVARCHAR(50) NOT NULL,                   -- 操作類型（如 INSERT, UPDATE, DELETE）
    ModifyUser NVARCHAR(50) NOT NULL,                  -- 修改使用者
    ModifyDate DATETIME NOT NULL DEFAULT GETDATE()     -- 修改日期，預設為當前時間
);
