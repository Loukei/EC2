# Demo

## 功能

利用`NorthWind`資料集，建立處理`Product`資料表的API

## 資料庫修改

`Product`添加以下屬性:

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

## 專案架構

### 展示層

- Controller
  - 接收HTTP request並做出適當的HTTP response回應
  - 操作Service來完成服務

### 商業邏輯層

- Repository
  - 利用`DapperContext`操作資料庫
  - 以Model層作為輸入或輸出，操作單一的Table

- Service
  - 組合多個資料表的操作來提供服務
  - 會使用1到多個Repostory來組合出我們的服務
  - 不涉及到HTTP相關的處理，這裡專注於輸出合適的資料物件

### 資料存取層

- ORMContext
  - 負責資料庫連線設定
  - DapperContext
- DTO
  - 映射資料庫表格的類別
  - Category
  - Product
  - Suppilier
- Models
  - ViewModel
  - 簡易驗證API使用者的參數
  - 隱藏不需要給User展示的參數
    - Ex:`Put`方法不需要提供`ProductID`
  - 作為API的Response Body，包裝輸出的資料

## API介紹

### Product API

#### `ServiceResponse`

一個資料類別，用來包裝`ProductController`的統一回應
只要CRUD成功，就會回傳`200 ok`的訊息，並且在request body顯示操作是否成功，以及錯誤訊息等。

#### `ServiceResponse Create(ProductViewModel product)`

依照使用者輸入產生對應的Product

#### `ServiceResponse GetAll(string? name, int? supplierID, int? categoryID, int pageIndex = 1, int pageSize = 10)`

根據輸入的條件搜尋適當的Products，並且回傳特定的分頁

#### `ServiceResponse Get(int productId)`

取出對應id的Product

#### `ServiceResponse Update(int productId,ProductViewModel product)`

依照`productId`更新對應的`ProductViewModel`資料

#### `ServiceResponse Delete(int productId)`

刪除對應的`productId`資料