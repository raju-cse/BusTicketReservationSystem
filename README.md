# Bus Ticket Reservation System

A user-friendly web application to search, book, and manage bus tickets efficiently, built with .NET 9 Web API, Angular 17, and PostgreSQL.

## 🖥️ Features
- ✅ User registration and login with JWT authentication  
- 🚌 Search buses by route and date  
- 🪑 View seat plans and book seats (login required)  
- 🔍 Filter and view bus details  
- 📝 Minimal admin management for buses and schedules  

## ⚙️ Tech Stack
- **Backend:** .NET 9 Web API with Entity Framework Core
- **Architecture:** Clean Architecture + Domain-Driven Design (DDD)   
- **Database:** PostgreSQL  
- **Frontend:** Angular 17 with Bootstrap 5

## 🚀 How to Run (Ubuntu)
1. Backend

   - Ensure PostgreSQL is running and update connection string in
     Backend/src/WebApi/appsettings.json if needed.
   - From repo Backend folder: dotnet restore dotnet build dotnet run --project
     src/WebApi/WebApi.csproj

2. Frontend
   - From Frontend/ClientApp: npm install npm start

## 📝 Author
- **Name:** Raju Ahmad  
- **Email:** razuahmed1066@gmail.com 





