# BackendSoftStu

## Install NuGet Package
From the Tools menu > select NuGet Package Manager > Package Manager Console (PMC).
- `Install-Package Microsoft.EntityFrameworkCore.Design`
- `Install-Package Microsoft.EntityFrameworkCore.SqlServer`

## Work with Migrations

#### Create migration
- `PM> Add-Migration <migiration-name>`

#### Updates the database to latest migration
- `PM> Update-Database`

#### Unapply a specific migration(s)
- `dotnet ef database update <previous-migration-name>`
or
- `PM> Update-Database -Migration <previous-migration-name>`

#### Unapply all migrations
- `dotnet ef database update 0`
or
- `PM> Update-Database -Migration 0`

#### Remove last migration
- `dotnet ef migrations remove`
or
- `PM> Remove-Migration`

#### Remove all migrations
- just remove Migrations folder.
