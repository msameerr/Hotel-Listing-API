# Hotel Listing API

This repository contains the source code for a **Web API** built using **.NET Core 6**. The project demonstrates RESTful principles, database integration, Swagger integration for API documentation, and the implementation of custom middleware to highlight the full capabilities of a .NET API.

## Technologies

The following technologies are utilized in this project:

- **.NET Core 6/7/8**
- **REST API design principles**
- **Swagger** for interactive API documentation
- **Entity Framework Core** for ORM and database interaction
- **SQL Server** as the database provider
- **Serilog** for structured logging

## Getting Started

To get the API up and running, follow these steps:

1. Clone this repository to your local environment.
2. Ensure you have the **.NET Core 6 SDK** and **SQL Server** installed on your system.
3. Modify the connection string in the `appsettings.json` file with your own SQL Server configuration.
4. Open the solution file (`.sln`) in **Visual Studio** or any compatible IDE.
5. Restore the required NuGet packages.
6. Set the **HotelListing.API** as the startup project.
7. Run the project.

## Project Structure

The project is organized into the following layers:

- **Core**: Contains business logic and interfaces.
- **Data**: Includes domain models and business rules.
- **API**: Hosts the Web API controllers, custom middleware, and route handling.

This layered structure separates concerns, ensuring scalability and maintainability.


## Contributing

Contributions are highly encouraged! If you would like to contribute a feature, fix, or improvement, please follow these steps:

1. **Fork** this repository.
2. Create a **new branch** for your feature or fix.
3. Implement your changes and commit them to your branch.
4. Submit a **pull request** for review.

-------------------------------------------------------------------------

## Swagger
 ![Swagger](https://github.com/user-attachments/assets/9e65b244-53c5-4a0a-9524-3f5cd4f574b6)
## Postman : JWT Authentication
![Postman](https://github.com/user-attachments/assets/d0aaff22-c317-4f05-8f20-d0b0632bae8f)
## Query String 
![QueryString](https://github.com/user-attachments/assets/170eba1c-4894-4bc4-84d6-8e34844d38a9)
## Pagging
![Pagging](https://github.com/user-attachments/assets/44a07b09-3224-4db6-8da4-84a773226d22)



