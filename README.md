# Free Spins Game API
A .NET 8 Web API for managing a promotional Free Spins Game. Players can spin during a limited-time campaign with a maximum number of spins per day. This project demonstrates clean CRUD, business logic, concurrency handling, and unit testing.

A PLAYER SHOULD BE SUBSCRIBED TO A CAMPAIGN TO BE ABLE TO SPIN!
Players can spin a limited number of times per day per campaign.
Tracks spin history with timestamps.
Optimistic concurrency handling using RowVersion.
Supports pagination for reading campaigns.
Unit tests cover spin logic, including concurrency.

## Technology Stack Section
1. .NET 8 - The core framework
2. ASP.NET Core Web API - For building the API
3. Entity Framework Core - For data access and ORM
4. SQL Server - The database
5. NUnit and InMemory database - For unit testing
6. AutoMapper - For entity mapping
7. JWT - For authentication and authorization

## Getting Started
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

## API Endpoints
SpinController
| Method   | Path                                              | Description                                         |
| -------- | ------------------------------------------------- | --------------------------------------------------- |
| **POST** | `/campaigns/{campaignId}/players/{playerId}/spin` | Execute a spin action for a player in a campaign    |
| **GET**  | `/campaigns/{campaignId}/players/{playerId}`      | Get status / progress of the player in the campaign |

PlayerController
| Method   | Path        | Description                                    |
| -------- | ----------- | ---------------------------------------------- |
| **POST** | `/register` | Register a new player                          |
| **POST** | `/login`    | Authenticate player and retrieve token         |

CampaignController
| Method     | Path                      | Description                                                |
| ---------- | ------------------------- | ---------------------------------------------------------- |
| **GET**    | `/all`                    | List all campaigns (supports query filters and pagination) |
| **GET**    | `/{campaignId}`           | Get details of a specific campaign                         |
| **POST**   | `/create`                 | Create a new campaign                                      |
| **PUT**    | `/{campaignId}`           | Update an existing campaign                                |
| **DELETE** | `/{campaignId}`           | Delete a campaign                                          |
| **POST**   | `/{campaignId}/subscribe` | Subscribe the current user/player to a campaign            |

