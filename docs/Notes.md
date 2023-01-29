

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

## Serivce 與生命週期(TODO)

在將ORM從Dapper 切換到 EFcore 時，需要把原本的repostory從`dapperconntext`改成使用`NorthWindContext`，此時遇到服務的生命週期問題

```shell
[18:16:08 FTL] Host terminated unexpectedly.
System.AggregateException: Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: EC2.Repository.ISuppilierRepository Lifetime: Singleton ImplementationType: EC2.Repository.SuppilierRepository': Cannot consume scoped service 'EC2.Models.EFCore.NorthwindContext' from singleton 'EC2.Repository.ISuppilierRepository'.) (Error while validating the service descriptor 'ServiceType: EC2.Service.IProductService Lifetime: Transient ImplementationType: EC2.Service.Implement.ProductService': Cannot consume scoped service 'EC2.Models.EFCore.NorthwindContext' from singleton 'EC2.Repository.ISuppilierRepository'.)
```

大致上的解法是將原本的repostory生命週期改成`Transient`

```cs
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ISuppilierRepository, SuppilierRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
```

- [在 ASP.NET Core Singleton 生命週期服務引用 Scoped 物件-黑暗執行緒](https://blog.darkthread.net/blog/aspnetcore-use-scoped-in-singleton/)
- [一图看懂 ASP.NET Core 中的服务生命周期 - 技术译民 - 博客园](https://www.cnblogs.com/ittranslator/p/asp-net-core-service-lifetimes-infographic.html)
- [ASP.NET Core MVC 生命週期介紹 - 理財工程師 Mars](https://blog.hungwin.com.tw/aspnet-core-mvc-lifecycle/)

---

## EFCore 實體跟蹤

在嘗試進行資料新增/修改等行為的時候，如果需要回傳執行結果，EFCore的實體跟蹤行為會將輸入的物件修改。
因此當User輸入值不包含Id的情況下，可以透過實體跟蹤的功能實現，傳回修改，創建的值

```cs
var p = new Product(); // assign value from user input, but no id contained
_db.Products.Add(p); // create product in db
_db.SaveChanges();
Console.WriteLine(p.id); // the p.id has already filled in
```


- [變更追蹤 - EF Core | Microsoft Learn](https://learn.microsoft.com/zh-tw/ef/core/change-tracking/#how-to-track-entities)
- [谈谈 EF CORE中的跟踪查询与非跟踪查询_David Hongyu的博客-CSDN博客_efcore tracking](https://blog.csdn.net/weixin_41372626/article/details/106266937)

---

### [問題]

DB schema的模型如果有 Not Null，但是經由EF core產生的資料卻會變成nullable

``` sql
# sql
[Status]          BIT           DEFAULT ((1)) NOT NULL,
```

```cs
class Product
{
    public bool? Status { get; set; }
}
```

- [Scaffold mapping on non-nullable bool · Issue #10840 · dotnet/efcore](https://github.com/dotnet/efcore/issues/10840)
- [EFCore 6.0 字串屬性對映欄位 NOT NULL 問題-黑暗執行緒](https://blog.darkthread.net/blog/efcore-6-nullable/)

---

## 物件循環引用

EFCore 自動生成的資料模型在遇到FK的時候會有循環引用的狀況，這會導致輸出結果的時候

``` txt
A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32. Consider using ReferenceHandler.Preserve on JsonSerializerOptions to support cycles. 
```

- [相關資料和序列化 - EF Core | Microsoft Learn](https://learn.microsoft.com/zh-tw/ef/core/querying/related-data/serialization)
- 
---

## EFCore query

### Find, Single, Where

Find方法會優先從緩存區域尋找值，因此速度更快，但是Find較缺乏彈性，只能通過pk做參數，想要修改其他參數則必須

- [DbContext.Find 方法 (Microsoft.EntityFrameworkCore) | Microsoft Learn](https://learn.microsoft.com/zh-tw/dotnet/api/microsoft.entityframeworkcore.dbcontext.find?view=efcore-6.0)
- [查詢和尋找實體 - EF6 | Microsoft Learn](https://learn.microsoft.com/zh-tw/ef/ef6/querying/#finding-an-entity-by-composite-primary-key)
- [.net - Entity Framework Find vs. Where - Stack Overflow](https://stackoverflow.com/questions/16966213/entity-framework-find-vs-where)

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

## Repostory Pattern 是否應該處裡連線錯誤?


- [The Repository Pattern Is About Reconstitution, Not (Only) Data Access and Flexibility | by Nicklas Millard | Level Up Coding](https://levelup.gitconnected.com/the-repository-pattern-is-about-reconstitution-not-only-data-access-and-flexibility-b3986380a248)
- [architecture - Should Repositories Throw Domain Errors - Stack Overflow](https://stackoverflow.com/questions/66480794/should-repositories-throw-domain-errors)
- [architecture - Why do Examples of the Repository Pattern never deal with Database Connection Exceptions? - Stack Overflow](https://stackoverflow.com/questions/30112403/why-do-examples-of-the-repository-pattern-never-deal-with-database-connection-ex)
- [Repository pattern and collection error handling - Stack Overflow](https://stackoverflow.com/questions/13108564/repository-pattern-and-collection-error-handling)
- [殷浩详解DDD系列 第三讲 - Repository模式-阿里云开发者社区](https://developer.aliyun.com/article/758292)

---

## AutoMapper

Repository層負責做資料庫的查詢動作，但是一般而言資料表的欄位並不會直接全部拿來輸出，例如外鍵(FK)的ID，以及提供給內部商業邏輯的欄位(CreateTime)等等。

當一個資料表的輸出欄位(`ProductDTO`)數量越多，將其轉換成需要的ViewModel(`ProductVM`)就需要大量的無腦工作。

AutoMapper這項套件可以幫我們節省大量的工作。

- [AutoMapper —— 類別轉換超省力 | 伊果的沒人看筆記本](https://igouist.github.io/post/2020/07/automapper/)
- [[創意料理] 用 Expression 做一個簡易的 Object-Object Mapping | 軟體主廚的程式料理廚房 - 點部落](https://dotblogs.com.tw/supershowwei/2020/06/15/144053)
- [AutoMapper vs Mapster in .NET - Code Maze](https://code-maze.com/automapper-vs-mapster-dotnet/)


### 另外一種手段

曾經考慮過要手動實現顯式轉型的擴充方法來實作

- [C# 筆記：擴充方法 - Huan-Lin 學習筆記](https://www.huanlintalk.com/2009/01/csharp-3-extension-methods.html)

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