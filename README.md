# Restaurant Management System

A modern, extensible Restaurant Management System built with ASP.NET Core Razor Pages and Entity Framework Core. This solution is designed to streamline restaurant operations, including menu management, order processing, table reservations, and sales tracking.

## Features

- **User Authentication & Authorization**  
  Integrated with ASP.NET Core Identity for secure user management and role-based access.

- **Menu Management**  
  Create, update, and categorize menu items with support for images, pricing, and availability.

- **Order Management**  
  Supports dine-in, takeout, and delivery orders. Tracks order status, special instructions, and delivery details.

- **Table Reservations**  
  Manage table availability and customer reservations with date and time tracking.

- **Sales Transactions**  
  Record and track payments, supporting multiple payment methods.

- **Audit & Activity Tracking**  
  Automatic tracking of creation and modification dates for all entities.

## Technology Stack

- **.NET 8**
- **C# 12**
- **ASP.NET Core Razor Pages**
- **Entity Framework Core**
- **SQL Server** (default, configurable)
- **ASP.NET Core Identity**

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or compatible database)
- Visual Studio 2022 (recommended)

### Setup Instructions

1. **Clone the Repository**
2. **Configure the Database**
- Update the connection string in `appsettings.json` under the `ConnectionStrings` section.

3. **Apply Migrations & Seed Data**
4. **Run the Application**

5. **Access the Application**
- Open your browser and navigate to `https://localhost:5001` (or the configured port).

## Project Structure

- `Domain`  
Core business models and interfaces.

- `Infrastructure`  
Data access, Entity Framework Core context, and repository implementations.

- `Application`  
Business logic, services, and background processing.

- `Presentation`  
Razor Pages UI and API endpoints.

## Key Classes

- **AppDbContext**  
Central EF Core context managing all entities, including users, menu items, orders, tables, and transactions.

- **AppUser**  
Extends IdentityUser for restaurant-specific user data.

- **Category, MenuItem, Order, OrderItem, Table, TableReservation, SalesTransaction**  
Core entities representing restaurant operations.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.

## Contact

For questions or support, please open an issue or contact the maintainer.
