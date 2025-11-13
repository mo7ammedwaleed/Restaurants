# Restaurants

> A sample RESTful API built with .NET 8 using Clean Architecture, CQRS, and modern tooling.

---

## Table of Contents

* [Project Overview](#project-overview)
* [Features](#features)
* [Architecture](#architecture)
* [Technologies](#technologies)
* [Prerequisites](#prerequisites)
* [Getting Started](#getting-started)

  * [Clone](#clone)
  * [Configuration](#configuration)
  * [Database (EF Core) Migrations & Seeding](#database-ef-core-migrations--seeding)
  * [Run the API](#run-the-api)
* [Swagger / API Documentation](#swagger--api-documentation)
* [Authentication & Authorization](#authentication--authorization)
* [Testing](#testing)
* [Logging & Exception Handling](#logging--exception-handling)

---

## Project Overview

This repository contains a sample restaurant management API built on **.NET 8**. The project demonstrates a production-like server-side application using clean architecture principles, CQRS patterns (via MediatR), Entity Framework Core for data access, and a number of cross-cutting concerns such as validation, logging, exception handling and API documentation.

The goal is to be a learning/reference project you can fork to study or reuse for new projects.

---

## Features

* .NET 8
* Entity Framework Core (SQL Server compatible)
* Clean Architecture (separation of concerns: API / Application / Domain / Infrastructure)
* CQRS with MediatR (commands & queries)
* Pagination and sorting for list queries
* Centralized logging
* Swagger (OpenAPI) for interactive documentation
* Global exception handling
* AutoMapper for DTO ⇄ domain mapping
* FluentValidation for request validation
* Authentication & Authorization using Microsoft Identity
* Unit and integration testing with xUnit

---

## Architecture

The solution follows Clean Architecture ideas:

* **Domain** — Entities, value objects, domain exceptions and domain interfaces.
* **Application** — Business logic, DTOs, MediatR handlers (commands/queries), validators, and interfaces used by infrastructure.
* **Infrastructure** — EF Core DbContext, repository implementations, external service implementations, identity persistence.
* **API (Web)** — Controllers, presentation layer, request/response models, DI registration, middleware (logging, exception handling, auth).

This structure keeps responsibilities separated so the core business rules can be tested independently of infrastructure concerns.

---

## Technologies

* .NET 8
* C#
* Entity Framework Core
* Microsoft Identity (ASP.NET Core Identity)
* MediatR
* AutoMapper
* FluentValidation
* Swagger / Swashbuckle
* xUnit
* Microsoft SQL Server

---

## Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* SQL Server (or any database provider supported by EF Core) or a local development alternative (LocalDB, Docker SQL image, etc.)
* (Optional) `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`

---

## Getting Started

### Clone

```bash
git clone https://github.com/mo7ammedwaleed/Restaurants.git
cd Restaurants
```

Open the solution (e.g. `Restaurants.sln`) in Visual Studio, VS Code, Rider or run with the CLI.

### Configuration

1. Copy the sample configuration (if present) to `appsettings.Development.json` or `appsettings.json`.
2. Update the connection string for your database. Example connection string for SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=RestaurantsDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

3. (If the project uses JWT or Identity settings) update any `Jwt` or `Identity` configuration values in `appsettings` accordingly.

### Database (EF Core) Migrations & Seeding

If the solution includes EF Core migrations, run the migrations to create the database schema:

```bash
# from the project that contains the DbContext (usually Infrastructure or Persistence project)
dotnet ef database update --project src/Infrastructure --startup-project src/Api
```

If there is a data seeding mechanism, it will typically run during application startup. Check the `Program.cs` / startup code to confirm.

### Run the API

From the API project folder (or from the solution root if startup project is set):

```bash
# run using the dotnet CLI
cd src/Api
dotnet run
```

When running in Development the Swagger UI should be available (see next section).

---

## Swagger / API Documentation

Swagger (OpenAPI) is available in development mode. After starting the app, open a browser and navigate to:

```
https://localhost:{PORT}/swagger
```

This UI provides an interactive way to explore endpoints, view request/response contracts, and test the API.

---

## Authentication & Authorization

This project uses Microsoft Identity for authentication and authorization. The Identity tables are typically persisted with EF Core. To test endpoints that require authentication:

1. Register a user (or seed a test user) using the provided identity endpoints (check `AuthController` / `AccountController`).
2. Log in to obtain a cookie or token depending on how Identity is configured.
3. Include the authentication token (e.g. `Authorization: Bearer <token>`) when calling protected endpoints.

Refer to the API controllers and `Program.cs` for exact routes and how JWT / cookies are configured.

---

## Testing

Run unit and integration tests with xUnit via the CLI from the solution root:

```bash
dotnet test
```

Look in the `tests/` folder for projects and test categories. Tests are typically organized to mirror the solution structure so you can run a single test project if desired.

---

## Logging & Exception Handling

The application includes centralized logging and global exception handling middleware. Check the `Program.cs` and `Middleware` folder for how logs and exceptions are wired up. You can configure logging providers (console, file, external sinks) in `appsettings` and during DI registration.

---
## Screen Shots

<img width="1920" height="1080" alt="Screenshot 2025-11-13 043816" src="https://github.com/user-attachments/assets/2dfb5aed-73d8-47ea-a57b-5ad28949b512" />
<img width="1920" height="1080" alt="Screenshot 2025-11-13 043856" src="https://github.com/user-attachments/assets/51f30317-0574-4e98-a89c-5fd7e5d7c174" />
<img width="1920" height="1080" alt="Screenshot 2025-11-13 043831" src="https://github.com/user-attachments/assets/e6083782-42bc-45a8-aa83-dbe25df01444" />
<img width="1920" height="1080" alt="Screenshot 2025-11-13 043845" src="https://github.com/user-attachments/assets/5c07c556-4eee-4784-b59e-b9093cd1180a" />
