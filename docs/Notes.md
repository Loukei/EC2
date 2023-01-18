

### Data annotation 

`Range(int, int)`

在為ViweModel添加範圍檢查的時候會使用類似以下程式碼，用來限定屬性的範圍。

``` C#
[Required]
[Range(1, int.MaxValue)]
public int ProductID { get; set; } = 1;
```

但是遇到`decimal`類型的時候常常會不知所措，因為以下聲明是非法的

```C#
[Range(1, decimal.MaxValue)]
```

原因是因為Range類別並沒有`decimal`的建構式

- [RangeAttribute 類別 (System.ComponentModel.DataAnnotations) | Microsoft Learn](https://learn.microsoft.com/zh-tw/dotnet/api/system.componentmodel.dataannotations.rangeattribute?view=net-7.0)

### ViewModel set default value

設定ViewModel的可視化資料的預設值

```
[Required]
[Range(1, int.MaxValue)]
[DefaultValue(1)]
public int ProductID { get; set; } = 1;
```

- `[DefaultValue(1)]`用來將Swagger網站開啟的時候提供的Exmaple input設定初始值
- 真正在設定初始值的方式為在建構子指定或是使用`=`處理

- [C#中默认值DefaultValueAttribute的误解_lpb914的博客-CSDN博客_c# defaultvalue](https://blog.csdn.net/lpb914/article/details/119751076)

## Paging 作法

- [ASP.net MVC + API Dapper 分頁寫法 | IT界的影武者 - 點部落](https://dotblogs.com.tw/bda605/2022/03/12/153046)
- [A Demo On .Net5 Web API Pagination Using Dapper ORM](https://www.learmoreseekmore.com/2021/08/demo-on-dotnet5-web-api-pagination-using-dapper-orm.html)
- [筆記－T-SQL 分頁查詢並傳回總筆數-黑暗執行緒](https://blog.darkthread.net/blog/tsql-paging-and-get-totalcount/)

---

## 參數繫結

對於controller，使用`GET`方法時，函式參數直接對應到URL參數上

`Get(int id)` -> `GET host/api/product?id=123`

如果查詢的參數非常多，則需要考慮使用建立ViewModel

```cs
[HttpGet("cars")]
public async Task<List<Car>> GetCars(string engineType, 
                                      int wheelsCount, 
                                  /*...20 params?...*/
                                         bool isTruck)
{
    //...
}
```

對於`Get`方法，使用非原始的資料型態做參數則必須加上`[FromQuery]`，以便將模型正確的轉換為URL參數

```cs
ServiceResponse GetAll([FromQuery] ProductPagingViewModel parameters)
```

- [使用 ASP.NET Core 建立 Web API | Microsoft Learn](https://learn.microsoft.com/zh-tw/aspnet/core/web-api/?view=aspnetcore-7.0#binding-source-parameter-inference-1)
- [c# - ASP.Net Core Web API Controller action with many parameters - Stack Overflow](https://stackoverflow.com/questions/50899602/asp-net-core-web-api-controller-action-with-many-parameters)

---

## learning resource

- [ASP.NET Core Overview - .NET/C#](https://baldur.gitbook.io/net-c/)

---

## 參考範例

- [Using Dapper with ASP.NET Core Web API - Code Maze](https://code-maze.com/using-dapper-with-asp-net-core-web-api/)
- [OUTPUT Clause (Transact-SQL) - SQL Server | Microsoft Learn](https://learn.microsoft.com/en-us/sql/t-sql/queries/output-clause-transact-sql?view=sql-server-ver16)


### Repository pattern

- [How to use Dapper with ASP.NET Core and Repository Pattern](https://blog.christian-schou.dk/how-to-use-dapper-with-asp-net-core/)

## API design guide & examples

- [REST API Best Practices – REST Endpoint Design Examples](https://www.freecodecamp.org/news/rest-api-best-practices-rest-endpoint-design-examples/)