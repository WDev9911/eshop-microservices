# ASP.NET Core 8 Microservices System

A modern microservices-based architecture built with ASP.NET Core 8, leveraging the latest .NET 8 and C# 12 features, designed for high scalability, performance, and maintainability.

---

## 🧱 Architecture & Patterns

- Vertical Slice Architecture with Feature Folders
- Domain-Driven Design (Entities, Value Objects, Aggregates)
- Clean Architecture and Separation of Concerns
- CQRS using MediatR
- Validation Pipeline with FluentValidation
- Design Patterns: Proxy, Decorator, Cache-aside

---

## 🛠 Technologies Used

- ASP.NET Core 8 & Minimal APIs
- C# 12 features
- MediatR for in-process messaging
- FluentValidation for input validation
- Marten (Document DB on PostgreSQL)
- Entity Framework Core (SQL Server) with Code-First Migrations
- Carter for minimal API endpoint routing
- Redis for distributed caching

---

## 🔗 Service Communication

- gRPC for high-performance inter-service calls (e.g., Basket ⇄ Discount)
- RabbitMQ with MassTransit for async event-driven communication
  - BasketCheckout queue
  - Topic Exchange model

---

## 🌐 API Gateway

- YARP (Yet Another Reverse Proxy) for gateway routing
- Refit for strongly-typed API consumption
- Rate limiting via `FixedWindowLimiter`

---

## 📦 Deployment & Infrastructure

- Dockerfile and docker-compose for multi-container setup
- PostgreSQL and Redis containerized
- Health checks and global exception handling
- Logging and observability integrations

---

## 🖥️ Frontend (UI)

- ASP.NET Core Web App using Razor & Bootstrap 4
- Consumes gateway APIs via Refit and HttpClientFactory

---

## ✅ Highlights

- DDD + CQRS + Clean Architecture best practices
- Fully containerized using Docker
- Modern .NET stack with focus on performance and maintainability

