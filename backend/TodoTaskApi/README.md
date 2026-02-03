# To-Do Task API

This project is a minimal To-Do task management API built with ASP.NET Core (.NET 9) and EF Core using SQLite for persistence.

## Features implemented
- CRUD endpoints for To-Do items
- SQLite persistence via EF Core
- Simple service layer and DbContext
- OpenAPI (Swagger) enabled

## Setup
1. Ensure .NET 9 SDK is installed.
2. From the `backend/TodoTaskApi` directory run:
   - `dotnet restore`
   - `dotnet run`
3. The API will start and create a `todo.db` SQLite file in the working directory.
4. Open `https://localhost:<port>/swagger` to explore the API (OpenAPI is enabled).

## Notes, assumptions, and trade-offs
- Data model is intentionally small: `ToDoItem` includes `Id`, `Title`, `Description`, `IsCompleted`, `CreatedAt`, and `DueDate`.
- For simplicity the project uses `EnsureCreated()` to create the database schema. For real production scenarios, use EF Core migrations.
- Authentication/authorization not implemented
- Minimal validation
