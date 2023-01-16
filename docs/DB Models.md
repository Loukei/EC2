# 資料集模型

雖然資料集是直接採用北風資料庫，但是基於某些功能實作的考量會需要更動schema，因此在這作個紀錄

## Version 1

實做Product API，添加幾個屬性

``` SQL
[Status]          BIT           DEFAULT ((1)) NOT NULL,
[CreatedDate]     DATETIME      DEFAULT (getdate()) NOT NULL,
[UpdatedDate]     DATETIME      NULL,
[CreatedBy]       INT           DEFAULT ((-1)) NOT NULL,
[UpdatedBy]       INT           NULL,
```

- Status: 設置資料刪除與否，0 表示該筆資料已刪除
- CreatedDate: 建立時間
- UpdatedDate: 最後修改時間
- CreatedBy: 資料的建立者
- UpdatedBy: 資料更新者


![Img](img/master%20-%20Northwind%20-%20dbo.png)

^ Power by BDreaver IDE