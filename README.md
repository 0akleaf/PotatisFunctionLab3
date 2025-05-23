# PotatisFunctionLab3

A simple Azure Functions project for managing "Potatis" (potato) entities using MongoDB as the backend database. The project is built with .NET 8 and C# 12.

## Features

- **Create Potatis**: Add a new potatis entry.
- **Get All Potatis**: Retrieve all potatis entries.
- **Get Potatis by ID**: Retrieve a specific potatis by its ID.
- **Update Potatis**: Update an existing potatis entry.
- **Delete Potatis**: Remove a potatis entry by ID.

## Requirements

- .NET 8 SDK
- Azure Functions Core Tools
- MongoDB instance (local or cloud)

## Setup

1. **Clone the repository**


2. **Configure MongoDB connection**
- Edit `local.settings.json` and set your MongoDB connection string and database name:
  ```json
  {
    "IsEncrypted": false,
    "Values": {
      "AzureWebJobsStorage": "UseDevelopmentStorage=true",
      "FUNCTIONS_WORKER_RUNTIME": "dotnet",
      "MongoDbConnectionString": "<your-mongodb-connection-string>",
      "MongoDbDatabaseName": "<your-database-name>"
    }
  }
  ```

3. **Run the project locally**


## API Endpoints

| Method | Route                | Description                |
|--------|--------------------- |----------------------------|
| POST   | /api/Potatis         | Create a new potatis       |
| GET    | /api/Potatis         | Get all potatis            |
| GET    | /api/Potatis/{id}    | Get potatis by ID          |
| PUT    | /api/Potatis/{id}    | Update potatis by ID       |
| DELETE | /api/Potatis/{id}    | Delete potatis by ID       |

## Potatis Model
```json
{
  "id": "string",        // MongoDB ObjectId
  "name": "string",      // Required
  "type": "string",      // Required
  "rank": "string"       // Required
}
```

## Example Request (Create Potatis)
```json
{
  "name": "King-Potato",
  "type": "Royal",
  "rank": "500"
}
```
