# PersonalBlog

PersonalBlog is a simple blog platform for sharing programming articles.  
It provides a backend API, application services, and a web frontend for reading and managing content.

## Features
- User registration and authentication
- Create, edit, and delete articles
- Comment on articles
- Tag management
- Role-based access control
- REST API for external access

## Project Structure
- `LoggerNLog/` – logging library using NLog
- `PersonalBlog.Api/` – API layer with controllers and middleware
- `PersonalBlog.Application/` – application services and DTOs
- `PersonalBlog.Core/` – domain models, interfaces, and value objects
- `PersonalBlog.Infrastructure/` – database context, migrations, repositories
- `PersonalBlog.Web/` – web frontend with views, controllers, and static files
- `PersonalBlog.sln` – solution file

## Technologies
- C#
- Entity Framework Core
- NLog for logging
- SQLite (local development)
- ASP.NET MVC for frontend
