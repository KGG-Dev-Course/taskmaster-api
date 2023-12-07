# TaskMaster API

TaskMaster API is a powerful ASP.NET Core Web API project designed for managing tasks efficiently.

## Features

- **Task Management:** Create, update, delete, and retrieve tasks.
- **Authentication:** Secure endpoints using JWT-based authentication.
- **Database:** Utilizes SQL Server for data storage.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT Authentication

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- SQL Server

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/chenxidev1129/taskmaster-api.git

2. Navigate to the project directory:
   ```bash
   cd taskmaster-api
   
4. Update the connection string:
   - Open ***appsettings.json*** and replace the ***ConnectionStrings*** with your SQL Server connection details:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=ADMIN\\SQL2022;Database=TaskMaster;Integrated Security=true;"
   }
   
6. Run the migration to apply the database schema:
   ```bash
   dotnet ef database update --context TaskMasterDbContext
   
8. Run the application:
   ```bash
   dotnet run

## API Endpoints
- **GET /api/tasks:** Retrieve all tasks.
- **GET /api/tasks/{id}:** Retrieve a specific task by ID.
- **POST /api/tasks:** Create a new task.
- **PUT /api/tasks/{id}:** Update an existing task.
- **DELETE /api/tasks/{id}:** Delete a task by ID.

## Contributing
Contributions are welcome! Please follow the guidelines outlined in CONTRIBUTING.md.

## License
This project is licensed under the MIT License.
