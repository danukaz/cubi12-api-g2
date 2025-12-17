# CUBI12 - API
API RESTful used as backend for Cubi12 software using .NET 7 and PostgreSQL.

Cubi12 is (currently) an open source project to help students at UCN (Universidad Cat�lica del Norte) at Chile to understand all the subjects they can take and their progress in the career.

## Prerequisites

- SDK [.NET 7](https://dotnet.microsoft.com/es-es/download/dotnet/7.0).
- Port 80 Available
- git [2.33.0](https://git-scm.com/downloads) or higher.
- [Docker](https://www.docker.com/) **Note**: If you do not have installed is highly recommended to see the steps to correctly install docker on your machine with [Linux](https://docs.docker.com/desktop/install/linux-install/), [Windows](https://docs.docker.com/desktop/install/windows-install/) or [MacOs](https://docs.docker.com/desktop/install/mac-install/).

## Database

The project uses **PostgreSQL** as the database engine.

For local development, PostgreSQL is run using Docker.
For production, the database will be deployed using Render.

Entity Framework Core is used as ORM, and database schema is managed through migrations.

## Getting Started

Follow these steps to get the project up and running on your local machine:

1. Clone the repository to your local machine.

2. Navigate to the root folder.
   ```bash
   cd Backend
   ```

3. Inside the project you will see 2 folders: Cubitwelve and Cubitwelve.tests, navigate to the first.
    ```bash
    cd Cubitwelve
    ```

4. Inside the folder Cubitwelve create a file and call it .env then fill with the following example values.
    ```bash
    DB_CONNECTION=Host=localhost;Port=5432;Database=cubi12;Username=postgres;Password=postgres;
    JWT_SECRET=your_jwt_secret_here
    ```

    **Note1:** `MSSQL_SA_PASSWORD` is no longer required since the project now uses PostgreSQL.
    
    **Note2**: If you change JWT_SECRET review [Padding](https://www.rfc-editor.org/rfc/rfc4868#page-5) of HmacSha256 Algorithm to avoid Runtime exceptions because of short secret.

5. Install project dependencies using dotnet sdk.
   ```bash
   dotnet restore
   ```

6. Follow the steps below (`Local Database Setup`).

## Local Database Setup (updated with PostgreSQL)

0. Follow the steps above (`Getting Started`) to setup the backend.

1. Start  PostgreSQL using Docker:
```bash
docker run --name postgres-cubi12 -e POSTGRES_DB=cubi12 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres:15
```

2. Apply database migrations:
```bash
dotnet ef database update
```

3. Run the backend:
```bash
dotnet run
```

## Use

To see the endpoints you can access to OpenAPI Swagger documentation at http://localhost:80/swagger/index.html

Also you can use [Postman](https://www.postman.com/) or another software to use the API.

## Database Persistence

The database container do not have a volume assigned, thus if you stop the container all the information will be deleted.

This behaviour is adopted because the Db container is designed only for development purposes.

## Testing

The project uses [xUnit](https://xunit.net/) as testing framework and [FluentAssertions](https://fluentassertions.com/) to improve readability, to run all the tests run the following commands:

1. If you are inside *Cubitwelve* folder go to root folder
    ```bash
    # Only if you are inside Cubitwelve folder
    cd ..
    ```
2. Now you are on the root folder, run the tests
    ```bash
    dotnet test
    ```