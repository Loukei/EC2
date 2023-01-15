

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

## 參考範例

- [Using Dapper with ASP.NET Core Web API - Code Maze](https://code-maze.com/using-dapper-with-asp-net-core-web-api/)
- [OUTPUT Clause (Transact-SQL) - SQL Server | Microsoft Learn](https://learn.microsoft.com/en-us/sql/t-sql/queries/output-clause-transact-sql?view=sql-server-ver16)

### Repository pattern

- [How to use Dapper with ASP.NET Core and Repository Pattern](https://blog.christian-schou.dk/how-to-use-dapper-with-asp-net-core/)