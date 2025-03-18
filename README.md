# Web API Extended Template

## v0.1.0

This is a flexible and extended .NET API template with a variety of pre-configured tools, designed to help you quickly spin up a scalable web API project. You can choose from different configurations like SQL Server, PostgreSQL, MongoDB, Swagger, and more.

## Prerequisites

- **.NET 8.0** or later
- A GitHub account
- An active internet connection to clone the template

## Installation

To install this template from GitHub, use the following steps:

1. Open your terminal or command prompt.
2. Run the following command to install the template globally:

   ```bash
   dotnet new --install WebApiExtendedTemplate
   ```

## Usage

Once the template is installed, you can create a new project using the following command:

```bash
dotnet new xtwebapi -n [YourProjectName]
```

Replace `[YourProjectName]` with the desired name for your new project.

## Template Parameters

When you run `dotnet new xtwebapi`, you can specify various options to customize the template according to your needs. Below are the available parameters:

### `sqlType`

Choose which type of SQL database to use with Entity Framework Core:

- `SqlServer` — Use SQL Server.
- `Postgres` — Use PostgreSQL.
- `MySQL` — Use MySql [(preview package)](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/9.0.0-preview.1)

- `InMemory` — Use an in-memory database.
- `none` — No SQL database (will exclude database-related files).

**Default value:** `InMemory`

### `useMongo`

Specify whether to use MongoDB in the project.

- `true` — Use MongoDB.
- `false` — Do not use MongoDB.

**Default value:** `false`

### `useAzureTable`

Specify whether to use Azure Table Storage.

- `true` — Use Azure Table.
- `false` — Do not use Azure Table.

**Default value:** `false`

### `endpoints`

Choose which kind of endpoints should be used:

- `controller` — Use controller-based endpoints.
- `minimal` — Use minimal APIs.

**Default value:** `controller`

### `useSwagger`

Specify whether to include Swagger API documentation:

- `true` — Enable Swagger documentation.
- `false` — Disable Swagger documentation.

**Default value:** `true`

### `useAzureAuth`

Specify whether to use Azure Active Directory for authentication.

- `true` — Enable Azure Authentication.
- `false` — Disable Azure Authentication.

**Default value:** `false`

### `useFirebaseAuth`

Specify whether to use Firebase Authentication.

- `true` — Enable Firebase Authentication.
- `false` — Disable Firebase Authentication.

**Default value:** `false`

## Example Usage

### Example 1: Creating a project with SQL Server, MongoDB, and no Swagger

```bash
dotnet new xtwebapi -n MyApiProject --sqlType SqlServer --useMongo --useSwagger false
```

### Example 2: Creating a project with PostgreSQL, no MongoDB, and Firebase Authentication

```bash
dotnet new xtwebapi -n MyApiProject --sqlType Postgres --useMongo --useFirebaseAuth
```

## Template Customization

The template can be customized with several conditions to include or exclude certain files based on your parameter choices. Some notable configurations include:

- **SQL Database Choices:** Depending on your selection for `sqlType`, the template will include or exclude files related to SQL Server, PostgreSQL, or in-memory databases.
- **Authentication:** You can choose between Azure or Firebase Authentication (coming soon), and the template will set up the appropriate services and configuration files.
- **EndPoints:** Depending on whether you choose controller-based or minimal APIs, the template will include the respective configuration files and remove the irrelevant ones.

## Uninstall

```bash
dotnet new uninstall WebApiExtendedTemplate
```

## Planned features

- **Azure Table Storage**
- **Message Queues** Auzure Service Bus and RabbitMQ
- **Object Stores** Azure Blob and Minio
