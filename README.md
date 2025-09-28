# Free Spins Game API
A .NET 8 Web API for managing a promotional Free Spins Game. Players can spin during a limited-time campaign with a maximum number of spins per day. This project demonstrates clean CRUD, business logic, concurrency handling, and unit testing.

Players can spin a limited number of times per day per campaign.
Tracks spin history with timestamps.
Optimistic concurrency handling using RowVersion.
Supports pagination for reading campaigns.
Unit tests cover spin logic, including concurrency.

Technology Stack Section
.NET 8 - The core framework
ASP.NET Core Web API - For building the API
Entity Framework Core - For data access and ORM
SQL Server - The database
NUnit and InMemory database - For unit testing
AutoMapper - For entity mapping
JWT - For authentication and authorization

Getting Started

1. Clone the repository: https://github.com/NikolaiAndrov/FreeSpinsGame.git
2. Enable Automatic Package Restore
This is the main setting to control automatic dependency installation.
Go to the menu bar and select Tools > NuGet Package Manager > Package Manager Settings.
In the Options dialog box, navigate to the NuGet Package Manager section.
Under this section, ensure that the following two options are checked:
Allow NuGet to download missing packages.
Automatically check for missing packages during build in Visual Studio.
3. Build solution.

4. Provide database and other configurations in appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<YOUR CONNECTION STRING>"
  },

  "JWT": {
    "Issuer": "https://localhost:<YOUR LOCALHOST PORT NUMBER>",
    "Audience": "https://localhost:<YOUR LOCALHOST PORT NUMBER>",
    "SigningKey": "<INSERT LONG STRING SEQUENCE>"
  },

  "Identity": {
    "SignIn": {
      "RequireConfirmedAccount": false
    },

    "Password": {
      "RequireUppercase": false,
      "RequireLowercase": false,
      "RequireNonAlphanumeric": false,
      "RequireDigit": false,
      "RequiredLength": 3
    }
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

5. Apply migrations.
6. Run the API.
7. Swagger UI should open automatically in the browser.
8. Database is seeded with a bit of data.
9. Test functionality:  CampaignId = 651d64a8-7378-4ee9-8916-776f2aa45d01, PlayerId = 151d64a8-7378-4ee9-8916-996f2aa45d01
