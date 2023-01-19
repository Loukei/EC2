# EC2 

- 使用asp.net core WebAPI 開發一個具有CRUD功能的API

---

## Screenshot

![Demo](docs/img/DEMO.png)

---

## 開發環境

- SDK: asp.net 6.0
- ORM: dapper
- Dataset: NorthWind
- DBMS: SQL server Express 2019

---

## 專案的分層架構

- [專案的分層架構](docs/%E5%B0%88%E6%A1%88%E6%9E%B6%E6%A7%8B.md)

---

## TODO

- API參數不能直接帶DB的Primary key
- GetAll 添加suplilierName參數
- 升級成.net core 7?
- Async非同步
- 國際化
- 使用者認證與限制
  - JWT
  - 防止server崩潰，需要對不同的User做出流量管制
- 添加測試
- 正確的處理HTTP request，並回應適當的http status code


---

