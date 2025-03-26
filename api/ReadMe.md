# API for Frontend

This project provides a backend API with database support and Swagger documentation.

## Setup

### 1. Create `appsettings.json`

Create a `appsettings.json` file in the project's root directory with the following content:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Host={host};Port={port};Username={username};Password={password};Database={database}"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*"
}
```

Replace `{host}`, `{port}`, `{username}`, `{password}`, and `{database}` with your actual database credentials.

### 2. Run the App Server (Swagger Enabled)

To start the application with hot reload and open Swagger UI, run:

```sh
dotnet watch run
```

Swagger UI will be available at: `http://localhost:<port>/swagger`

## Database Setup

### 1. Create Migrations

Run the following command to create a new database migration:

```sh
dotnet ef migrations add InitialMigration
```

### 2. Apply Migrations (Create Tables)

Run this command to update the database with the latest migrations:

```sh
dotnet ef database update
```

## Notes
- Ensure you have `Entity Framework Core` installed and configured before running migration commands.
- Replace placeholders in `appsettings.json` with your actual database connection details.

---


