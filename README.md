# 🛒 BazaR — Marketplace Platform

BazaR is a full-featured e-commerce marketplace built with **ASP.NET Core MVC (.NET 10)**, Entity Framework Core, and ASP.NET Identity. It supports buying and selling goods, order management, a built-in wallet, a bonus system, wishlists, reviews, and more — structured around clean layered architecture principles.

> 🚀 **Deployed to Azure:** [BazaRFinal on Azure Web App](https://github.com/arkadiychulkover/BazaR) via GitHub Actions CI/CD

---

## 📋 Table of Contents

- [Features](#-features)
- [Architecture](#-architecture)
- [Domain Models](#-domain-models)
- [Business Logic](#-business-logic)
- [Tech Stack](#-tech-stack)
- [Getting Started](#-getting-started)
- [Testing](#-testing)
- [CI/CD](#-cicd)
- [Authors](#-authors)

---

## ✨ Features

- **Authentication & Authorization** — ASP.NET Identity with email/password and **Google OAuth** login
- **Product Catalog** — Browse, filter, and search items by category, brand, color, country, seller type, and custom characteristics
- **Shopping Cart** — Add items, manage quantities, and proceed to checkout
- **Wishlist** — Save items for later
- **Orders** — Full order lifecycle: New → Processing → Shipped → Delivered / Cancelled
- **Wallet** — Built-in user balance for payments and transaction history
- **Bonus System** — Automatic bonus accrual on purchases; deducted on cancellation
- **Reviews** — Users can leave reviews on purchased items
- **Delivery** — Supports Nova Poshta, Ukr Poshta, courier, and self-pickup; 22 Ukrainian cities
- **Mailing** — Email notifications via MailKit with background mailing service
- **Admin Panel** — Manage users, items, and platform-wide settings
- **Premium Subscriptions** — Optional premium user accounts
- **Health Checks** — Per-controller and database health monitoring endpoints
- **Activity Tracking** — Logs page visits, cart additions, wishlist additions, orders, and browsing behavior
- **Active Users Tracking** — Real-time online users via singleton `ActiveUsersService`

---

## 🧱 Architecture

BazaR follows **Layered Architecture**:

```
Presentation    →  Controllers, Views, ViewComponents (MVC)
Application     →  Services, Filters, Helpers, ViewModels
Domain          →  Models, Enums, Interfaces, DTOs
Infrastructure  →  EF Core, AppDbContext, Repositories, Migrations
```

### Key Design Patterns

- **Repository Pattern** — `IUserDb`, `IItemRepository`, `ILogDb`, `IUserStatistick` abstract data access
- **Action Filters** — `UserContextFilter`, `LoggerActionFilter`, `BlockResourseFilter`, `OnlineResourceFilter` applied globally
- **View Components** — `MobileMoreMenuViewComponent` for reusable UI rendering
- **Background Services** — `MailingBackgroundService` for async email delivery
- **Health Checks** — Dedicated `IHealthCheck` implementations per controller and for the database

---

## 🧠 Domain Models

| Model | Description |
|---|---|
| `User` | Extends `IdentityUser<int>`; has profile info, roles, and related collections |
| `Item` | Product with price, images, brand, category, colors, delivery options, services |
| `Order` | Belongs to a user; has status, payment method, delivery method, and items |
| `OrderItem` | Junction between Order and Item with quantity/price snapshot |
| `CartItem` | Temporary cart entry per user |
| `WishlistItem` | Saved item per user |
| `Review` | User review on an item |
| `BonusAccount` | Tracks bonus balance per user |
| `WalletTransaction` | Financial transaction record |
| `Delivery` | Available delivery options per item |
| `PremiumSubscription` | Premium membership record |
| `Pet` | User-linked pet profile (for pet supply category features) |
| `UserUseStatistick` | Tracks user actions (visits, cart, wishlist, orders, browsing) |

**Key Relationships:**
- `User` → `Orders` (1:N), `CartItems` (1:N), `WishlistItems` (1:N), `Reviews` (1:N), `SellingItems` (1:N)
- `Order` → `OrderItems` (1:N)
- `Item` → `Category`, `Brand`, `Colors`, `Reviews`, `Characteristics`, `Deliveries`

---

## 💼 Business Logic

### 📦 Orders
- Create order from cart with `OrderItems` snapshot
- Payment methods: Pay Now, Pay on Delivery, Card at Branch
- Delivery: Nova Poshta, Ukr Poshta, Nova Poshta Courier, Self-Pickup
- Status flow: `New → Processing → Shipped → Delivered` or `Cancelled`
- On cancellation: order status updated, bonuses deducted, payment refunded

### 🎁 Bonus System
- Bonuses credited automatically on successful purchase
- Deducted proportionally on order cancellation
- Balance cannot go negative

### 💰 Wallet
- Each user has one wallet
- Used for payments and refunds
- Full transaction history via `WalletTransaction`

### 📬 Mailing
- Email notifications triggered by user events
- `MailingGeneratorService` creates email content
- `MailingBackgroundService` processes the queue asynchronously
- Sent via MailKit (SMTP)

---

## 🔐 Security

- ASP.NET Identity with configurable password policy (min. 6 chars)
- Google OAuth 2.0 login
- Account lockout after 5 failed attempts (1-year lockout)
- Unique email enforcement
- HttpOnly, SameSite=Lax cookies with 12-hour sliding expiration
- `BlockResourseFilter` — blocks access for restricted users

---

## 🛠 Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| Language | C# |
| ORM | Entity Framework Core 10 |
| Database | SQL Server |
| Identity | ASP.NET Identity + Google OAuth |
| Email | MailKit |
| Testing | xUnit (unit tests) |
| CI/CD | GitHub Actions → Azure Web App |
| Hosting | Microsoft Azure |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or remote)

### Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/arkadiychulkover/BazaR.git
   cd BazaR
   ```

2. **Configure `appsettings.json`:**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=BazaR;Trusted_Connection=True;"
     }
   }
   ```

3. **Apply migrations** (runs automatically on startup, or manually):
   ```bash
   dotnet ef database update
   ```

4. **Run the project:**
   ```bash
   dotnet run --project BazaR/BazaR
   ```

> ⚠️ The app runs on x64 on Windows to avoid `BadImageFormat` issues with native SQL client dependencies.

---

## 🧪 Testing

Unit tests are located in the `BazaR.Tests` project:

```bash
dotnet test
```

Test coverage includes:
- `AccountControllerTests`
- `AdminControllerTests`
- `CartControllerTest`
- `WishListControllerTest`
- `BlockFilterTest`, `UserContextFilterTests`, `OnlineResourceFilterTests`, `LoggerActionFilterTests`

---

## ⚙️ CI/CD

Automated deployment is configured via **GitHub Actions**:

- Trigger: push to `Arkadii` branch or manual dispatch
- Build: `dotnet build --configuration Release` on Windows runner (.NET 10)
- Deploy: Published artifact deployed to **Azure App Service** (`BazaRFinal`, Production slot)

Workflow file: [`.github/workflows/arkadii_bazarfinal.yml`](.github/workflows/arkadii_bazarfinal.yml)

---

## 👨‍💻 Authors

**ALI Team**

GitHub: [@arkadiychulkover](https://github.com/arkadiychulkover)
