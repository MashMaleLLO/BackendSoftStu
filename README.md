# BackendSoftStu

## Work with migrations
#### To unapply a specific migration(s):
- `dotnet ef database update <previous-migration-name>`
or
- `PM> Update-Database -Migration <previous-migration-name>`

#### To unapply all migrations:
- `dotnet ef database update 0`
or
- `PM> Update-Database -Migration 0`

#### To remove last migration:
- `dotnet ef migrations remove`
or
- `PM> Remove-Migration`

#### To remove all migrations:
- just remove Migrations folder.