using lab6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Вибір бази даних через конфігурацію
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
{
    switch (databaseProvider)
    {
        case "SqlServer":
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"),
                            npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorCodesToAdd: null));
            break;
        case "Postgres":
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            break;
        case "Sqlite":
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
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
