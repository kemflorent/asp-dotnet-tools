## Create new ASP.NET project
dotnet new webapp --name crudwebapp
## Launch project in production profile by commandline
dotnet run --launch-profile "Environments-Prod"
## Drop database
dotnet ef database drop
## Make new migration to create tables
dotnet ef migrations add InitialCreate
## Delete migration
dotnet ef migrations remove
## Create Database
dotnet ef database update