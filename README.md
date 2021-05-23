# Install .NET 5 
[.NET 5 Download](https://dotnet.microsoft.com/download/dotnet/5.0)
# Install Entity Framework
    dotnet tool install --global dotnet-ef
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.Tools
# Migrate Sqlite Server   
```
dotnet ef migrations add "Initial Migrations"
dotnet ef database update
```
# Run Web API
```
dotnet build
dotnet run
```
