# MechanicShop - Clean Architecture

This project demonstrates a Mechanic Shop management system built using ASP.NET Core and Clean Architecture principles.

## Features

- Customer and vehicle management
- Service booking and tracking
- Mechanic assignment
- Invoice generation

## Technologies

- ASP.NET Core
- Entity Framework Core
- Clean Architecture (Domain, Application, Infrastructure, Presentation layers)

## Structure

```
/Domain         # Business models and logic
/Application    # Use cases and interfaces
/Infrastructure # Data access and external services
/Presentation   # API controllers and UI
```

## Getting Started

1. Clone the repository.
2. Restore NuGet packages.
3. Update database connection strings.
4. Run migrations: `dotnet ef database update`
5. Start the application: `dotnet run`

## Contributing

Pull requests are welcome. Please follow the coding standards and submit issues for bugs or feature requests.

## License

This project is licensed under the MIT License.