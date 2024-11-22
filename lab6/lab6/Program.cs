using lab6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configuration of DbContext
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dbHost = "localhost";
var dbName = "lab6";

// Add DbContext with the correct provider based on runtime configuration
builder.Services.AddDbContext<DataContext>(options =>
{
    switch (databaseProvider)
    {
        case "SqlServer":
            connectionString = $"Server={dbHost};Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
            options.UseSqlServer(connectionString);
            break;
        case "Postgres":
            var dbUser = "postgres";
            var dbPassword = "postgres";
            connectionString = $"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPassword};";
            options.UseNpgsql(connectionString);
            break;
        case "Sqlite":
            connectionString = $"Data Source={dbHost};";
            options.UseSqlite(connectionString);
            break;
        case "InMemory":
            options.UseInMemoryDatabase("EventManagementDb");
            break;
        default:
            throw new Exception("Unsupported database provider");
    }
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
