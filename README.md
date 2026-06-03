<h1 align="center">🛒 ECommerce</h1>

<p align="center">
  <em>Clean Architecture · ASP.NET Core Web API · .NET 8</em>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
</p>

---

## 📖 About

A robust **e-commerce backend API** built with **.NET 8** and **Clean Architecture**. Designed for scalability and maintainability, the solution separates business logic, domain rules, and infrastructure concerns into clearly defined layers.

- 🏗️ Built on **Clean Architecture** principles
- ⚡ RESTful API powered by **ASP.NET Core**
- 🗄️ Data persistence via **Entity Framework Core** & **SQL Server**
- 🧩 Fully decoupled layers for easy testing and extension

---

## 🏛️ Architecture

```
ECommerce.Api  ──►  ECommerce.Application  ──►  ECommerce.Domain
                           ▲
               ECommerce.Infrastructure
```

| Layer | Responsibility |
|---|---|
| **Api** | HTTP entry point — controllers, middleware, routing |
| **Application** | Use cases, business logic, DTOs, service interfaces |
| **Domain** | Core entities, value objects, domain rules (no dependencies) |
| **Infrastructure** | EF Core, repositories, external services, DB migrations |

---

## 🛠️ Tech Stack

### Languages & Frameworks
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

### Database & ORM
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

### Tools & Platforms
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)
![Git](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or remote)
- Visual Studio 2022 (v17.13+) or VS Code

### 1. Clone the repository

```bash
git clone https://github.com/TarekElabsy222/ECommerce.git
cd ECommerce
```

### 2. Configure the connection string

Edit `ECommerce.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ECommerceDb;Trusted_Connection=True;"
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

The API will be available at `https://localhost:5001`

---

## 📁 Solution Structure

```
ECommerce/
├── ECommerce.sln
├── ECommerce.Api/
│   └── ECommerce.Api.csproj
├── ECommerce.Application/
│   └── ECommerce.Application.csproj
├── ECommerce.Domain/
│   └── ECommerce.Domain.csproj
└── ECommerce.Infrastucture/
    └── ECommerce.Infrastructure.csproj
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
