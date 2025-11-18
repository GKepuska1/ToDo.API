# TODO API with CQRS Pattern

## Architecture Overview

This solution implements a clean CRUD API for TODO items using the CQRS (Command Query Responsibility Segregation) pattern.

### Project Structure

- **ToDo.Domain** - Domain entities
  - `ToDoItem.cs` - Main TODO entity with properties: Id, Title, Description, IsCompleted, CreatedAt, CompletedAt, UpdatedAt

- **ToDo.Core** - Application core with queries and DbContext
  - `ToDoDbContext.cs` - EF Core DbContext with InMemory database
  - `Queries/ToDoQueries.cs` - Query definitions (GetAll, GetById, GetCompleted, GetPending)
  - `Queries/ToDoQueryHandlers.cs` - Query handlers

- **ToDo.Commands** - CQRS commands and handlers
  - `ToDoCommands.cs` - Command definitions (Create, Update, Delete, Complete)
  - `ToDoCommandHandlers.cs` - Command handlers

- **ToDo.API** - Web API layer
  - `Controllers/ToDoController.cs` - REST API controller
  - `Program.cs` - Application configuration

## API Endpoints

### Queries (GET)
- `GET /api/todo` - Get all TODO items
- `GET /api/todo/{id}` - Get TODO by ID
- `GET /api/todo/completed` - Get completed TODOs
- `GET /api/todo/pending` - Get pending TODOs

### Commands (POST/PUT/PATCH/DELETE)
- `POST /api/todo` - Create new TODO
- `PUT /api/todo/{id}` - Update TODO
- `PATCH /api/todo/{id}/complete` - Mark TODO as completed
- `DELETE /api/todo/{id}` - Delete TODO

## Technologies Used

- **.NET 10** - Latest .NET framework
- **MediatR 12.4.1** - CQRS implementation
- **Entity Framework Core 9.0.0** - ORM with InMemory database
- **Swashbuckle/Swagger** - API documentation
- **C# 14.0** - Modern C# features (records, required properties)

## Running the Application

1. Build the solution
2. Run the API project
3. Navigate to Swagger UI (typically https://localhost:5001/swagger)
4. Test the endpoints

## CQRS Pattern Benefits

- **Separation of Concerns** - Commands modify data, Queries read data
- **Scalability** - Can optimize reads and writes independently
- **Maintainability** - Clear boundaries between operations
- **Testability** - Easy to unit test handlers

## Database

Currently using **InMemory database** for development. To use a real database:
1. Install appropriate EF Core provider (SQL Server, PostgreSQL, etc.)
2. Update `Program.cs` to use the provider
3. Add connection string to `appsettings.json`
4. Run migrations

## Next Steps

- Add validation using FluentValidation
- Implement authentication/authorization
- Add pagination for list queries
- Replace InMemory database with persistent storage
- Add unit and integration tests
- Implement logging and error handling middleware
