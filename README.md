# PawTrack
A .NET-based veterinary appointment tracking system built with a three-layer architecture.

## Overview
PawTrack is a robust solution for managing veterinary appointments and pet healthcare records. Built with C# and .NET, it follows a clean three-layer architecture pattern separating data access, business logic, and presentation layers.

## Architecture
The solution consists of three projects:

### PawTrack.Data
- Data access layer
- Handles database operations 
- Manages entity models and repositories
- Database context and migrations

### PawTrack.Business
- Business logic layer
- Implements core business rules
- Service implementations
- Data validation and processing

### PawTrack.WebApi 
- Presentation layer
- RESTful API endpoints
- Authentication and authorization
- Request/response handling

## Tech Stack
- **Framework**: .NET Core
- **Language**: C#
- **Project Type**: Web API
- **Architecture**: 3-Layer Architecture 
- **Database**: Entity Framework Core

## Setup
1. Clone the repository
2. Ensure .NET SDK is installed
3. Update connection string in `appsettings.json`
4. Run migrations:
In Package Manager Console:
```bash
Add-Migration Initial PawTrack.Data
```
5. Update Database:
In Package Manager Console:
```bash
Update-Database 
```
6. Build solution (Ctrl + Shift + B)
7. Set PawTrack.WebApi as startup project
8. Press F5 to run the application

## Prerequisites

- .NET SDK 7.0 or later
- SQL Server or compatible database
- Visual Studio 2022 or VS Code

## Development
To add new features:

1. Add data models to Data layer
2. Implement business logic in Business layer
3. Create API endpoints in WebApi layer
4. Follow existing patterns and conventions

## Testing
1. Open Test Explorer (Test > Test Explorer)
2. Click "Run All" to execute all tests
3. View test results in Test Explorer window

## License
MIT License
Copyright (c) 2025 PawTrack


