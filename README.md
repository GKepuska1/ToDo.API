# TODO API with CQRS Pattern

## Architecture Overview

This solution implements a clean CRUD API for TODO items using the CQRS pattern with DTOs and AutoMapper.

### Project Structure

- **ToDo.Domain** - Domain entities and DTOs
  - `Entities/ToDo/ToDoItem.cs` - Main TODO entity with properties: Id, Title, Description, IsCompleted, CreatedAt, CompletedAt, UpdatedAt
  - `Dtos/ToDoDto.cs` - Data Transfer Objects:
    - `ToDoDtoGet` - Used for returning data to clients
    - `ToDoDtoCreate` - Used for creating new TODO items
    - `ToDoDtoUpdate` - Used for updating existing TODO items

- **ToDo.Core** - Application core with services, queries and DbContext
  - `Contexts/ToDoDbContext.cs` - EF Core DbContext with InMemory database
  - `Services/ToDoService.cs` - Business logic and data access
  - `Mappings/ToDoMappingProfile.cs` - AutoMapper configuration for entity-DTO conversion

- **ToDo.Commands** - CQRS commands and queries with handlers
  - `ToDo/CreateToDoCommand.cs` - Create command and handler
  - `ToDo/UpdateToDoCommand.cs` - Update command and handler
  - `ToDo/DeleteToDoCommand.cs` - Delete command and handler
  - `ToDo/CompleteToDoCommand.cs` - Complete command and handler
  - `ToDo/Queries/GetAllToDosQuery.cs` - Query and handler for all items
  - `ToDo/Queries/GetToDoByIdQuery.cs` - Query and handler for single item
  - `ToDo/Queries/GetCompletedToDosQuery.cs` - Query and handler for completed items
  - `ToDo/Queries/GetPendingToDosQuery.cs` - Query and handler for pending items

- **ToDo.API** - Web API layer
  - `Controllers/ToDoController.cs` - REST API controller
  - `Program.cs` - Application configuration

## API Endpoints

### Queries (GET) - Returns DTOs
- `GET /api/todo` - Get all TODO items → Returns `List<ToDoDtoGet>`
- `GET /api/todo/{id}` - Get TODO by ID → Returns `ToDoDtoGet`
- `GET /api/todo/completed` - Get completed TODOs → Returns `List<ToDoDtoGet>`
- `GET /api/todo/pending` - Get pending TODOs → Returns `List<ToDoDtoGet>`

### Commands (POST/PUT/PATCH/DELETE)
- `POST /api/todo` - Create new TODO (accepts `ToDoDtoCreate`)
- `PUT /api/todo/{id}` - Update TODO (accepts `ToDoDtoUpdate`)
- `PATCH /api/todo/{id}/complete` - Mark TODO as completed
- `DELETE /api/todo/{id}` - Delete TODO

## Technologies Used

- **.NET 10** - Latest .NET framework
- **MediatR 12.4.1** - CQRS implementation
- **Entity Framework Core 9.0.0** - ORM with InMemory database
- **AutoMapper 12.0.1** - Object-to-object mapping
- **Swashbuckle/Swagger** - API documentation
- **C# 14.0** - Modern C# features (but none used)

## Running the Application

1. Build the solution
2. Run the API project
3. Navigate to Swagger UI (typically https://localhost:5001/swagger)
4. Test the endpoints

## Architecture Benefits

### CQRS Pattern
- **Separation of Concerns** - Commands modify data, Queries read data
- **Scalability** - Can optimize reads and writes independently
- **Maintainability** - Clear boundaries between operations
- **Testability** - Easy to unit test handlers

### DTOs with AutoMapper
- **API Contract Stability** - Internal entity changes don't affect API consumers
- **Security** - Control what data is exposed to clients
- **Flexibility** - Different representations for different operations (Get/Create/Update)
- **Automatic Mapping** - AutoMapper eliminates manual mapping code

**Command Flow:**
1. Controller receives DTO
2. CQRS command handler maps DTO to entity
3. Handler calls Service with entity
4. Service performs operation via DbContext
5. Controller returns result

## Database

Currently using **InMemory database** for development. To use a real database