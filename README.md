# Bookify API

**Bookify** is a Web API built using ASP.NET Core, showcasing a wide range of skills in building modern web applications. This project demonstrates proficiency with various tools and technologies such as Dapper, EF Core, Redis, and Keycloak, while adhering to Domain-Driven Design (DDD) and CQRS patterns. 

The purpose of this project is to showcase my ability to build scalable, maintainable, and well-architected applications using clean architecture principles.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Setup](#setup)
- [Authentication](#authentication)
- [Background Jobs](#background-jobs)
- [Logging and Monitoring](#logging-and-monitoring)
- [Cross-Cutting Concerns](#cross-cutting-concerns)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Domain-Driven Design (DDD)**: Implements entities and value objects with a focus on persistence ignorance.
- **CQRS with MediatR**: Command-Query Responsibility Segregation using MediatR for better scalability.
- **PostgreSQL**: Database management with Dapper and EF Core for both raw and ORM-based access.
- **Outbox Pattern**: Reliable message processing to ensure no data is lost during communication between services.
- **Authentication & Authorization**: 
  - Keycloak for authentication.
  - Role-based and permission-based access control.
- **Background Processing**: Quartz.NET for scheduling background tasks.
- **Caching**: Redis for distributed caching.
- **Logging & Monitoring**: 
  - Serilog for structured logging.
  - Seq for log aggregation and visualization.
- **Docker Compose**: Simplifies managing all services (database, Redis, Keycloak, Seq) in development and production environments.

## Technologies

- **ASP.NET Core**: Core framework.
- **Dapper & Entity Framework Core**: Data access.
- **MediatR**: For implementing CQRS and handling application logic.
- **Quartz.NET**: For background job scheduling.
- **Serilog**: Structured logging.
- **PostgreSQL**: Primary database.
- **Redis**: Distributed caching layer.
- **Seq**: Log aggregation and visualization.
- **Keycloak**: Authentication and authorization.
- **Docker Compose**: Service orchestration.

## Architecture

The **Bookify** API follows Clean Architecture principles with a clear separation of concerns:

- **Domain Layer**: Implements core business logic with DDD, using entities and value objects, adhering to persistence ignorance.
- **Application Layer**: Uses CQRS to separate reads from writes, along with MediatR to handle requests and implement cross-cutting concerns.
- **Infrastructure Layer**: Handles data access with Dapper and EF Core, and background jobs using Quartz.
- **API Layer**: Exposes endpoints to clients.


## Setup

### Prerequisites

- [.NET 6+](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/get-started) and [Docker Compose](https://docs.docker.com/compose/install/)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Redis](https://redis.io/download)
- [Seq](https://datalust.co/seq)

### Running the Application

1. Clone the repository:

    ```bash
    git clone https://github.com/your-username/bookify.git
    cd bookify
    ```

2. Start the services using Docker Compose:

    ```bash
    docker-compose up --build
    ```

## Authentication

**Bookify** uses [Keycloak](https://www.keycloak.org/) for authentication.
Role-based and permission-based authorization are implemented from scratch to control access to specific resources and actions.

### Keycloak Setup

- Import the provided Keycloak realm configuration from the `keycloak/` folder.
- Make sure to configure the API to communicate with the Keycloak instance by setting the proper environment variables in `appsettings.json`.

## Background Jobs

Quartz.NET is used to schedule and run background tasks like notifications and periodic updates. Jobs are configured in the Infrastructure layer and can be added dynamically.

## Logging and Monitoring

- **Serilog** is used for structured logging across the application.
- Logs are stored and visualized using **Seq**.

To view the logs:

- Open Seq at `http://localhost:5341` (default) after running the services.

## Cross-Cutting Concerns

Using **MediatR** pipelines, various cross-cutting concerns are managed, including:

- **Logging**: Logs the execution of commands and queries.
- **Validation**: Ensures incoming requests are valid before proceeding.
- **Caching**: Optimizes query performance by caching responses where appropriate.

## Contributing

Contributions are welcome! Please submit a pull request or open an issue to discuss any changes or improvements.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

