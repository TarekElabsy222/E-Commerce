<h1 align="center">🛒 ECommerce API</h1>

<p align="center">
  <em>Clean Architecture · ASP.NET Core Web API · .NET 9 · 73 Endpoints</em>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET_9-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Stripe-635BFF?style=for-the-badge&logo=stripe&logoColor=white" />
  <img src="https://img.shields.io/badge/AutoMapper-BE4B48?style=for-the-badge&logo=automapper&logoColor=white" />
  <img src="https://img.shields.io/badge/MailKit-0078D4?style=for-the-badge&logo=maildotru&logoColor=white" />
  <img src="https://img.shields.io/badge/Serilog-B22222?style=for-the-badge&logo=serilog&logoColor=white" />
  <img src="https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" />
  <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" />
</p>

---

## 📖 About

A robust, production-ready **e-commerce backend API** built with **.NET 9** and **Clean Architecture**. The API exposes **73 endpoints** covering all e-commerce operations — from product browsing and cart management to payments and order tracking.

- 🏗️ Built on **Clean Architecture** principles
- ⚡ **73 RESTful endpoints** across all modules
- 🛡️ JWT Authentication with Refresh Tokens
- 💳 **Stripe** payment integration
- 📧 Email notifications via **MailKit**
- 📋 Structured logging with **Serilog**
- 🔄 Object mapping with **AutoMapper**
- ♻️ **Generic Repository** pattern for clean data access
- 🚨 Global **Exception Handling** middleware
- 🗄️ **SQL Server** + **Entity Framework Core**

---

## ✨ Features

| Feature | Description |
|---|---|
| 🔐 **Authentication & Authorization** | JWT access tokens + refresh token rotation |
| 🛍️ **Product Management** | CRUD with categories, brands, stock, and discounts |
| 🛒 **Shopping Cart** | Add, update, remove cart items per user |
| ❤️ **Wishlist** | Create and manage named wishlists |
| 📦 **Order Management** | Place orders, track status, manage shipping |
| 💳 **Stripe Payments** | Secure payment processing via Stripe |
| ⭐ **Reviews & Ratings** | Customers can rate and review products |
| 📧 **Email Notifications** | Transactional emails via MailKit (SMTP) |
| 📋 **Serilog Logging** | Structured, queryable logs with sinks |
| 🚨 **Global Exception Handling** | Consistent error responses across all endpoints |
| 🔄 **AutoMapper** | Clean DTO ↔ Entity mappings |
| ♻️ **Generic Repository** | Reusable, testable data access layer |

---

## 🛠️ Tech Stack

### Languages & Frameworks
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 9](https://img.shields.io/badge/.NET_9-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

### Database & ORM
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

### Libraries & Integrations
![Stripe](https://img.shields.io/badge/Stripe-635BFF?style=for-the-badge&logo=stripe&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-BE4B48?style=for-the-badge&logo=automapper&logoColor=white)
![MailKit](https://img.shields.io/badge/MailKit-0078D4?style=for-the-badge&logo=maildotru&logoColor=white)
![Serilog](https://img.shields.io/badge/Serilog-B22222?style=for-the-badge&logo=serilog&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

### Tools & Platforms
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)
![Git](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)

---

## 🗺️ API Endpoints Overview

> **73 endpoints** across 9 modules

| Module | Endpoints | Description |
|---|---|---|
| 🔐 Auth | Register, Login, Refresh Token, Revoke, Email Confirm… |
| 🛍️ Products | List, Get, Create, Update, Delete, Filter, Search… |
| 🗂️ Categories |CRUD + nested/parent category support |
| 🏷️ Brands | CRUD + brand product listing |
| 🛒 Cart | Get cart, Add item, Update quantity, Remove item, Clear |
| ❤️ Wishlist | Create, Get, Add item, Remove item, Delete wishlist |
| 📦 Orders | Place order, Get orders, Get by ID, Update status |
| ⭐ Reviews | Add, Get, Update, Delete review per product |
| 💳 Payments | Create session, Confirm, Refund, Payment history |

---

## 🏛️ Architecture

```
ECommerce.Api  ──►  ECommerce.Application  ──►  ECommerce.Domain
                           ▲
               ECommerce.Infrastructure
```

| Layer | Responsibility |
|---|---|
| **Api** | HTTP entry point — controllers, middleware, routing, exception handler |
| **Application** | Use cases, business logic, DTOs, AutoMapper profiles, service interfaces |
| **Domain** | Core entities, value objects, domain rules (no dependencies) |
| **Infrastructure** | EF Core, generic repository, Serilog, MailKit, Stripe, migrations |

---

## 🗄️ Database Design

```mermaid
erDiagram
  APPLICATION_USER {
    Guid Id PK
    string FirstName
    string LastName
    string Email UK
    string PasswordHash
    string Address
  }
  REFRESH_TOKEN {
    string Token
    datetime ExpiresOn
    datetime CreatedOn
    datetime RevokedOn
    Guid UserId FK
  }
  PRODUCT {
    Guid Id PK
    string Name
    decimal Price
    int StockAmount
    decimal DiscountPercentage
    Guid CategoryId FK
    Guid BrandId FK
  }
  CATEGORY {
    Guid Id PK
    string Name
    Guid ParentCategoryId FK
  }
  BRAND {
    Guid Id PK
    string Name
    string Email
    string Phone
  }
  ORDER {
    Guid Id PK
    datetime Date
    string Status
    string ShippingAddress
    double ShippingCost
    Guid CustomerId FK
  }
  ORDER_ITEMS {
    Guid Id PK
    int Quantity
    double TotalPrice
    double Discount
    Guid OrderId FK
    Guid ProductId FK
  }
  CART {
    Guid Id PK
    Guid CustomerId FK
  }
  CART_ITEMS {
    Guid Id PK
    int Quantity
    Guid CartId FK
    Guid ProductId FK
  }
  WISHLIST {
    Guid Id PK
    string Name
    Guid CustomerId FK
  }
  WISHLIST_ITEMS {
    Guid Id PK
    Guid WishlistId FK
    Guid ProductId FK
  }
  REVIEW {
    Guid Id PK
    int Rate
    string Comment
    Guid CustomerId FK
    Guid ProductId FK
  }
  PAYMENT {
    Guid Id PK
    string Method
    string Currency
    double Amount
    Guid CustomerId FK
  }

  APPLICATION_USER ||--o{ REFRESH_TOKEN : "has"
  APPLICATION_USER ||--o{ ORDER : "places"
  APPLICATION_USER ||--o{ CART : "owns"
  APPLICATION_USER ||--o{ WISHLIST : "owns"
  APPLICATION_USER ||--o{ REVIEW : "writes"
  APPLICATION_USER ||--o{ PAYMENT : "makes"
  CATEGORY ||--o{ PRODUCT : "contains"
  CATEGORY ||--o| CATEGORY : "parent of"
  BRAND ||--o{ PRODUCT : "owns"
  ORDER ||--o{ ORDER_ITEMS : "has"
  PRODUCT ||--o{ ORDER_ITEMS : "in"
  CART ||--o{ CART_ITEMS : "has"
  PRODUCT ||--o{ CART_ITEMS : "in"
  WISHLIST ||--o{ WISHLIST_ITEMS : "has"
  PRODUCT ||--o{ WISHLIST_ITEMS : "in"
  PRODUCT ||--o{ REVIEW : "has"
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or remote)
- Stripe account (for payment keys)
- SMTP credentials (for MailKit)
- Visual Studio 2022 (v17.13+) or VS Code

### 1. Clone the repository

```bash
git clone https://github.com/TarekElabsy222/ECommerce.git
cd ECommerce
```

### 2. Configure `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ECommerceDb;Trusted_Connection=True;"
  },
  "JWT": {
    "Key": "your-secret-key",
    "Issuer": "ECommerceApi",
    "Audience": "ECommerceClient",
    "DurationInDays": 1
  },
  "Stripe": {
    "SecretKey": "sk_test_...",
    "PublishableKey": "pk_test_..."
  },
  "MailSettings": {
    "Host": "smtp.your-provider.com",
    "Port": 587,
    "Email": "your@email.com",
    "Password": "your-password",
    "DisplayName": "ECommerce"
  },
  "Serilog": {
    "MinimumLevel": "Information"
  }
}
```

### 3. Apply database migrations

```bash
dotnet ef database update --project ECommerce.Infrastucture --startup-project ECommerce.Api
```

### 4. Run the API

```bash
dotnet run --project ECommerce.Api
```

Swagger UI: `https://localhost:5001/swagger`

---

## 📁 Solution Structure

```
ECommerce/
├── ECommerce.sln
│
├── E-Commerce.Api
│   ├── Connected Services
│   ├── Dependencies
│   ├── Properties
│   ├── wwwroot
│   ├── Controllers
│   ├── log
│   ├── appsettings.json
│   ├── E-Commerce.Api.http
│   └── Program.cs
│
├── E-Commerce.Application
│   ├── Dependencies
│   ├── DependencyInjection
│   ├── DTOs
│   ├── Mapping
│   └── Services
│
├── E-Commerce.Domain
│   ├── Dependencies
│   ├── Entities
│   └── Repositories
│
└── E-Commerce.Infrastructure
    ├── Dependencies
    ├── Data
    ├── DependencyInjection
    ├── MiddleWare
    ├── Migrations
    ├── Repositories
    └── Services
```

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

---

## 👨‍💻 Author

<p align="center">
  <a href="https://github.com/TarekElabsy222">
    <img src="https://img.shields.io/badge/GitHub-TarekElabsy222-181717?style=for-the-badge&logo=github" />
  </a>
</p>

---

<p align="center">
  <em>"Clean code always looks like it was written by someone who cares." – Robert C. Martin</em>
</p>
