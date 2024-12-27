# BlockedDomain


## Overview
Blocked Domain is a sophisticated and scalable application designed to manage a collection of external domains that cannot be visited by users of a local network. If a domain is blocked, any of its subdomains are also blocked. This project incorporates best practices, SOLID principles, advanced architecture, and the latest technologies to ensure high performance, maintainability, and scalability.

## Table of Contents
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [SOLID Principles](#solid-principles)
- [Features](#features)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [Testing](#testing)
- [Advanced Enhancements](#advanced-enhancements)
- [Contributing](#contributing)
- [License](#license)

## Technologies Used
- .NET Core 5/6
- Entity Framework Core
- ASP.NET Core Identity
- Autofac for Dependency Injection
- Hangfire for Background Processing
- Serilog for Logging
- Swagger/OpenAPI for API Documentation
- Polly for Resilience and Retry Policies
- Redis for Caching
- Hangfire for Asynchronous Processing
- Docker for Containerization
- Azure/AWS for Cloud Deployment

## Architecture
This project follows Clean Architecture principles to separate concerns and enhance maintainability. The layers are:
- **Core**: Contains business logic and domain entities.
- **Application**: Contains interfaces and use cases.
- **Infrastructure**: Contains data access implementations.
- **WebApi**: Contains the presentation layer and API controllers.

### Project Structure
```
/TodoSolution
    /Core
        /Entities
            BlockedDomain.cs
            Host.cs
    /Application
        /Interfaces
            IBlockedDomainService.cs
            IUnitOfWork.cs
            IBlockedDomainRepository.cs
        /UseCases
            BlockedDomainService.cs
    /Infrastructure
        /Data
            BlockedDomainContext.cs
            BlockedDomainRepository.cs
            Trie.cs
            TrieNode.cs
            TrieDomainBlockChecker.cs
            UnitOfWork.cs
    /WebApi
        /Controllers
            BlockedDomainController.cs
        /Configurations
            AutofacModule.cs
    /Tests
        /UnitTests
            BlockedDomainServiceTests.cs
        /IntegrationTests
            BlockedDomainIntegrationTests.cs
        /EndToEndTests
            BlockedDomainEndToEndTests.cs
    Program.cs
    Startup.cs
```

## SOLID Principles
This project adheres to SOLID principles:
- **Single Responsibility Principle (SRP)**: Each class has a single responsibility, such as domain entities, services, and repositories.
- **Open/Closed Principle (OCP)**: Classes are open for extension but closed for modification. For example, the `Trie` class can be extended with additional features without modifying existing code.
- **Liskov Substitution Principle (LSP)**: Subtypes can be substituted for their base types. For instance, different implementations of `IDomainBlockChecker` can be used interchangeably.
- **Interface Segregation Principle (ISP)**: Interfaces are client-specific and not overly general. Separate interfaces are used for different responsibilities.
- **Dependency Inversion Principle (DIP)**: Depend on abstractions rather than concrete implementations. Dependency injection is managed using Autofac.

## Features
- **Domain Management**: Add, block, and check domains and their subdomains.
- **Caching**: Use Redis for efficient data caching.
- **Background Processing**: Handle long-running tasks using Hangfire.
- **Health Checks**: Monitor the application's health and ensure it is running smoothly.
- **Logging**: Use Serilog for comprehensive logging.
- **Security**: Implement authentication and authorization using ASP.NET Core Identity.
- **API Documentation**: Use Swagger/OpenAPI for documenting the API.
- **Asynchronous Processing**: Handle asynchronous tasks using Hangfire.
- **Containerization**: Use Docker for consistent deployment environments.
- **Cloud Deployment**: Deploy to cloud platforms like Azure or AWS for scalability and reliability.

## Setup and Installation
### Prerequisites
- .NET Core 5/6 SDK
- SQL Server
- Redis
- Docker (optional)

### Installation
1. **Clone the repository**:
    ```sh
    git clone https://github.com/yourusername/blocked-domain-api.git
    cd blocked-domain-api
    ```

2. **Install dependencies**:
    ```sh
    dotnet restore
    ```

3. **Configure database connection**:
    Update the `appsettings.json` file with your SQL Server connection string.

4. **Run the application**:
    ```sh
    dotnet run
    ```

5. **Build Docker image (optional)**:
    ```sh
    docker build -t blocked-domain-api .
    ```

## Usage
### API Endpoints
- **Add Blocked Domain**: `POST /api/BlockedDomain`
- **Check if Domain is Blocked**: `GET /api/BlockedDomain/isBlocked?domain=example.com`
- **Get All Blocked Domains**: `GET /api/BlockedDomain`

### Example Request
#### Add Blocked Domain
```sh
curl -X POST "https://localhost:5001/api/BlockedDomain" -H "accept: text/plain" -H "Content-Type: application/json" -d "{\"domain\":\"example.com\"}"
```

## Testing
### Running Tests
To run the unit, integration, and end-to-end tests:
```sh
dotnet test
```

### Test Structure
- **Unit Tests**: Test individual components in isolation (`/Tests/UnitTests`).
- **Integration Tests**: Test interactions between components (`/Tests/IntegrationTests`).
- **End-to-End Tests**: Test the entire flow of the application (`/Tests/EndToEndTests`).

## Advanced Enhancements
### Exception Handling and Resilience
- **Global Exception Handling**: Middleware to catch and log unhandled exceptions.
- **Retry Policies**: Use Polly to implement retry policies for transient failures.

### Performance Tuning
- **Response Compression**: Enable response compression to reduce HTTP response size.
- **Database Tuning**: Use indexes and optimize queries for better performance.

### Documentation and API Versioning
- **Swagger**: Use Swagger/OpenAPI for documenting the API.
- **API Versioning**: Implement API versioning to manage breaking changes.

### Audit Logging
- **Entity Framework Interceptors**: Log database operations for audit purposes.

### Microservices and Event-Driven Architecture
- **Event Bus**: Use messaging systems like RabbitMQ for decoupling services.
- **API Gateway**: Use Ocelot as an API gateway for aggregating service calls.

## Contributing
1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

## License
This project is licensed under the MIT License.

---

## Improvements 
GraphQL for efficient data fetching.

ML.Net for Machine learning.
