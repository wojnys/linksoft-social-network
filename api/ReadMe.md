### API pro frontend

### Create appsetings.json

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

### Run app server command (swagger )

## `dotnet watch run (open swagger) `

## DB

### create migrations

### `dotnet ef migrations add init`

### create tables

### `dotnet ef database update`


```
