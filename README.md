# EC2 

- 使用asp.net core WebAPI 開發一個具有CRUD功能的API

---

## Screenshot

![Demo](docs/img/DEMO.png)

---

## 開發環境

- SDK: asp.net 6.0
- ORM: EF Core
- Dataset: NorthWind
- DBMS: SQL server Express 2019

---

## 專案的分層架構

- [專案的分層架構](docs/%E5%B0%88%E6%A1%88%E6%9E%B6%E6%A7%8B.md)

---

## Awesome example with NorthWind REST API

- [alperenalp/learning-web-api-with-asp.net-core: HTTP Metotları (GET, POST, PUT, PATCH, DELETE) kullanarak API uçları oluşturdum. Örnek olarak hazır SQL bir veritabanı olan Northwind veritabanını kullandım.](https://github.com/alperenalp/learning-web-api-with-asp.net-core)
- [NORTHWIND_DATAGROKR_ASSIGNMENT](https://documenter.getpostman.com/view/12122001/T1DnidZm#03816d73-4d89-482a-a720-21774204ec75)

---

## TODO

- 改用EF core
  - 修改Repository
    - Product
      - 解決循環引用的問題
        - 建立一個新的DTO，裡面不使用循環參考，避免循環引用
          - [Create Data Transfer Objects (DTOs) | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5)
          - [Select specific properties from include ones in entity framework core - Stack Overflow](https://stackoverflow.com/questions/46476117/select-specific-properties-from-include-ones-in-entity-framework-core)
        - 安裝NewtonJSON套件  
      - 可能要修改repository的介面
      - 更新時UpdateBy UpdateDate要跟著修改
    - Category
    - Suppilier
  - 測試功能
- API參數不能直接帶DB的Primary key
- GetAll 添加suplilierName參數
- 升級成.net core 7?
- Async非同步
- 國際化
- 使用者認證與限制
  - JWT
  - 防止server崩潰，需要對不同的User做出流量管制
- CQRS
- 添加測試
- 正確的處理HTTP request，並回應適當的http status code


---

