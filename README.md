# Bus Ticket Reservation System

## 📝 Author Info

- Name: Raju Ahmad
- Email: razuahmed1066@gmail.com

What's included:

- Backend (.NET 9 Web API) with PostgreSQL via EF Core
- JWT Authentication (register/login)
- Search buses, view seat plan, book seat (booking requires login)
- Frontend (Angular 17) minimal UI using Bootstrap CDN

How to run (Ubuntu):

1. Backend

   - Ensure PostgreSQL is running and update connection string in
     Backend/src/WebApi/appsettings.json if needed.
   - From repo Backend folder: dotnet restore dotnet build dotnet run --project
     src/WebApi/WebApi.csproj

2. Frontend
   - From Frontend/ClientApp: npm install npm start

Notes:

- The project is scaffolded for development convenience. The JWT secret and DB
  password are for demo only. Change them before production.
