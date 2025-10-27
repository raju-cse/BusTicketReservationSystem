
using Application.Contracts.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Configuration (Postgres)
var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Host=localhost;Database=BusTicketReservation;Username=postgres;Password=password;Port=5432";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(conn));

// JWT Settings
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? "ThisIsASecretKeyForDevelopmentOnlyChangeIt";
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISearchService, Application.Services.SearchService>();
builder.Services.AddScoped<IBookingService, Application.Services.BookingService>();
builder.Services.AddScoped<Application.Contracts.Interfaces.IBusScheduleRepository, BusScheduleRepository>();
builder.Services.AddScoped<Application.Contracts.Interfaces.ITicketRepository, TicketRepository>();
builder.Services.AddScoped<Application.Contracts.Interfaces.IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<Application.Contracts.Interfaces.IBusRepository, BusRepository>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    await SeedData.Initialize(context);
}

app.Run();
