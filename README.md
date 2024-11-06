# Game of Drones App
This is a backend of Game of Drones developed by dotnet 8.0

---
## API Collections
API Collections to get history
```
GET /api/game/winners: Retrieve all winners history.
```

API Collections to get history
```
GET api/game/moves: Retrieve all moves.
POST api/game/move: Create a new move.
	body:
	{
		"Name": "String",
		"Kills": "Dog"
	}
PUT api/game/move/{id}: Update a move.
	body:
	{
		"Name": "Dog",
		"Kills": "Paper"
	}

DELETE api/game/move/{id}: Delete a move by ID.
```

## Installation

1. Execute this script to create the database in sql server
```sql
CREATE DATABASE GAME_OF_DRONES
GO

USE GAME_OF_DRONES
GO

DROP TABLE IF EXISTS GAME_OF_DRONES.dbo.Games
DROP TABLE IF EXISTS GAME_OF_DRONES.dbo.Moves
GO

CREATE TABLE GAME_OF_DRONES.dbo.Moves (
    MoveId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Kills NVARCHAR(50) NOT NULL UNIQUE
)
GO

CREATE TABLE GAME_OF_DRONES.dbo.Winners (
    WinnerId INT PRIMARY KEY IDENTITY(1,1),
    WinnerName NVARCHAR(50) NOT NULL,
    DatePlayed DATETIME DEFAULT GETDATE()
)
GO

INSERT INTO GAME_OF_DRONES.dbo.Moves
(Name, Kills)
VALUES('Paper', 'Rock')

INSERT INTO GAME_OF_DRONES.dbo.Moves
(Name, Kills)
VALUES('Rock', 'Scissors')

INSERT INTO GAME_OF_DRONES.dbo.Moves
(Name, Kills)
VALUES('Scissors', 'Paper')
GO
```
---
# Configuration
Change the string connection appsettings.json

For Connection String:
```json
"DefaultConnection": "Server=tcp:localhost,1433;Database=GAME_OF_DRONES;User Id=sa;Password=StrongP@ssw0rd!;Encrypt=False;"
```

Allow cors:
```json
"AllowedOrigins": "http://localhost:4200"
```
