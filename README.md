# Project Summary

## Overview
This is a **Todo application** built with **.NET 10.0** using **Clean Architecture** principles.  
It exposes a RESTful API with full CRUD operations for managing Todo items.

**All code in this project was generated end.to.end by _Google Antigravity_ in under 30 minutes, from prompt to final implementation.**

---

## Architecture

The solution is organized into three primary projects.

### Dotted.Api
- Entry point of the application.  
- Built with **ASP.NET Core Minimal APIs**.  
- Handles HTTP requests and responses.  
- Configures dependency injection and middleware.

### Dotted.Core
- Core domain layer.  
- Contains the `TodoItem` entity.  
- Defines the `ITodoRepository` interface, following the Repository Pattern.  
- Has zero dependencies on other projects.

### Dotted.Infrastructure
- Infrastructure layer implementation.  
- Implements `ITodoRepository` with an in-memory `InMemoryTodoRepository`.  
- Includes `DataSeeder` for initial data population.

---

## Features

| Action | HTTP Method | Endpoint |
|--------|-------------|----------|
| Get all todos | `GET` | `/todos` |
| Create todo | `POST` | `/todos` |
| Complete todo | `PUT` | `/todos/{id}/complete` |
| Delete todo | `DELETE` | `/todos/{id}` |

---

## Technology Stack
- **Framework:** .NET 10.0  
- **API Style:** Minimal APIs  
- **Data Storage:** In-memory collection (development / testing)  
- **Documentation:** OpenAPI / Swagger via `Microsoft.AspNetCore.OpenApi`

---

## Quick Start
1. Restore and run the API.  
```bash
dotnet restore
dotnet run --project Dotted.Api
