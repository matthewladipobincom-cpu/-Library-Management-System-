# Library Management System

A comprehensive ASP.NET Core Web API for managing a library system with JWT authentication, role-based authorization, and SQL Server database integration.

## Features

- **User Authentication & Authorization**: JWT-based authentication with role-based access control (Admin/User roles)
- **Book Management**: Full CRUD operations for books
- **Database Integration**: Entity Framework Core with SQL Server
- **API Documentation**: Swagger/OpenAPI documentation
- **Secure Password Handling**: Password hashing and verification
- **External Book Service Integration**: Integration with external book APIs

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or Azure SQL Database
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with C# extension

## Setup Instructions

### 1. Clone the Repository

```bash
git clone <repository-url>
cd -Library-Management-System-
```

### 2. Database Setup

The application uses SQL Server. You have two options:

#### Option A: Use Azure SQL Database (Current Configuration)
The project is configured to use an Azure SQL Database. The connection string is already set in `appsettings.json`.

#### Option B: Use Local SQL Server
If you prefer to use a local SQL Server instance, update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Install Dependencies

```bash
dotnet restore
```

### 4. Apply Database Migrations

```bash
dotnet ef database update
```

This will create the necessary database tables and apply any pending migrations.

### 5. Configure JWT Settings (Optional)

The JWT settings are already configured in `appsettings.json`. If you need to change them:

```json
{
  "Jwt": {
    "Key": "YourSecretKeyHere",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience",
    "DurationInMinutes": 60
  }
}
```

**Important**: Keep the JWT key secure and don't commit it to version control in production.

## Running the Application

### Development Mode

```bash
dotnet run
```

The application will start on `https://localhost:7133` and `http://localhost:5171`.

### Using Visual Studio

1. Open the solution file (`.sln`) in Visual Studio
2. Set the startup project to `-Library-Management-System-`
3. Press F5 or click "Run"

### Using VS Code

1. Open the project folder in VS Code
2. Open the integrated terminal
3. Run `dotnet run`

## API Documentation

Once the application is running, visit:
- **Swagger UI**: `http://localhost:5171/swagger` or `https://localhost:7133/swagger`
- **OpenAPI JSON**: `http://localhost:5171/swagger/v1/swagger.json`

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token

### Books (Requires Authentication)
- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get book by ID
- `POST /api/books` - Add new book (Admin only)
- `PUT /api/books/{id}` - Update book (Admin only)
- `DELETE /api/books/{id}` - Delete book (Admin only)

### Admin Endpoints
- `GET /api/auth/admin` - Admin-only endpoint

## Testing the API

### 1. Register a User

```bash
curl -X POST "http://localhost:5171/api/auth/register" \
     -H "Content-Type: application/json" \
     -d '{
       "fullName": "John Doe",
       "email": "john@example.com",
       "password": "password123",
       "role": "User"
     }'
```

### 2. Login

```bash
curl -X POST "http://localhost:5171/api/auth/login" \
     -H "Content-Type: application/json" \
     -d '{
       "email": "john@example.com",
       "password": "password123"
     }'
```

This will return a JWT token. Copy the token for authenticated requests.

### 3. Access Protected Endpoints

```bash
curl -X GET "http://localhost:5171/api/books" \
     -H "Authorization: Bearer YOUR_JWT_TOKEN_HERE"
```

## Project Structure

```
-Library-Management-System-/
├── Controllers/           # API Controllers
│   ├── AuthController.cs
│   └── BooksController.cs
├── Data/                  # Database Context
│   └── AppDbContext.cs
├── Helpers/               # Utility Classes
│   ├── JwtTokenGenerator.cs
│   └── PasswordHelper.cs
├── Interfaces/            # Service Interfaces
├── Migrations/            # EF Core Migrations
├── Model/                 # Data Models
│   ├── Books.cs
│   ├── User.cs
│   └── RegisterRequest.cs
├── Repositories/          # Data Access Layer
├── Services/              # Business Logic Layer
├── Properties/
│   └── launchSettings.json
├── appsettings.json       # Configuration
├── Program.cs             # Application Entry Point
└── -Library-Management-System-.csproj
```

## Technologies Used

- **ASP.NET Core 9.0**: Web API framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Database
- **JWT Bearer Authentication**: Token-based authentication
- **Swashbuckle.AspNetCore**: API documentation
- **Microsoft.AspNetCore.Authentication.JwtBearer**: JWT authentication middleware

## Development Notes

- The application uses nullable reference types (`<Nullable>enable</Nullable>`)
- Implicit usings are enabled (`<ImplicitUsings>enable</ImplicitUsings>`)
- Passwords are hashed using secure hashing algorithms
- JWT tokens expire after 60 minutes by default

## Troubleshooting

### Common Issues

1. **Database Connection Error**
   - Ensure SQL Server is running
   - Check the connection string in `appsettings.json`
   - Run `dotnet ef database update` to apply migrations

2. **JWT Authentication Issues**
   - Verify the JWT settings in `appsettings.json`
   - Ensure the token is included in the Authorization header as `Bearer {token}`

3. **Port Already in Use**
   - Change the ports in `Properties/launchSettings.json`
   - Or stop other applications using the same ports

### Build Errors

If you encounter build errors:

```bash
# Clean and rebuild
dotnet clean
dotnet build
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Run tests
5. Submit a pull request

## License

This project is licensed under the MIT License.
