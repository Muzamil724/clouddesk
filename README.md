# CloudDesk

A cloud-native IT support ticket API, built with ASP.NET Core. This is Day 1: a
working CRUD API running locally against a SQLite database. Later steps will
move it to Azure App Service + Azure SQL, add Blob Storage attachments, Key
Vault secrets, Application Insights monitoring, Bicep infrastructure-as-code,
and a GitHub Actions CI/CD pipeline.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

## Run it locally

```bash
cd CloudDesk
dotnet restore
dotnet run
```

The console output will show the URLs the app is listening on (something like
`http://localhost:5000`). Open:

```
http://localhost:5000/swagger
```

Swagger gives you an interactive page where you can try every endpoint without
writing any client code.

## Endpoints

| Method | Route               | Description                          |
|--------|---------------------|---------------------------------------|
| GET    | /api/tickets        | List all tickets (optional `?status=`) |
| GET    | /api/tickets/{id}   | Get a single ticket                   |
| POST   | /api/tickets        | Create a ticket                       |
| PUT    | /api/tickets/{id}   | Update a ticket                       |
| DELETE | /api/tickets/{id}   | Delete a ticket                       |

### Example: create a ticket

```json
POST /api/tickets
{
  "title": "VPN not connecting",
  "description": "User cannot connect to the office VPN from home.",
  "priority": "High",
  "assignedTo": "muzamil"
}
```

## How the database works right now

On startup, `Program.cs` calls `Database.EnsureCreated()`, which creates a
`clouddesk.db` SQLite file with the `Tickets` table if it doesn't exist yet.
This is fine for quick local development.

Once we move to Azure SQL (Day 3-4), we'll switch to EF Core **migrations**
instead, which is the standard way to manage schema changes against a real
database:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Project structure

```
CloudDesk/
├── Controllers/
│   └── TicketsController.cs   # CRUD endpoints
├── Data/
│   └── CloudDeskContext.cs    # EF Core DbContext
├── Models/
│   └── Ticket.cs              # Ticket entity
├── Program.cs                 # App startup/configuration
├── appsettings.json
└── CloudDesk.csproj
```

## Roadmap

- [x] Day 1-2: Local CRUD API with SQLite
- [ ] Day 3-4: Deploy to Azure App Service + Azure SQL Database
- [ ] Day 5: Ticket attachments via Azure Blob Storage
- [ ] Day 6: Secrets in Azure Key Vault + managed identity
- [ ] Day 7: Application Insights monitoring
- [ ] Day 8-9: Bicep infrastructure-as-code
- [ ] Day 10-11: GitHub Actions CI/CD
- [ ] Day 12: README polish, screenshots, resume write-up
