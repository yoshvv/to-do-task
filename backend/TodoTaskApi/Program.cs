using Microsoft.EntityFrameworkCore;
using TodoTaskApi.Data;
using TodoTaskApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure CORS - allow any origin for development/MVP. In production restrict origins.
var corsPolicyName = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure EF Core with SQLite (file-based). In production this could be configured via connection string.
var connectionString = builder.Configuration.GetConnectionString("TodoDb") ?? "Data Source=todo.db";
builder.Services.AddDbContext<ToDoDbContext>(options => options.UseSqlite(connectionString));

// Register application services
builder.Services.AddScoped<IToDoService, ToDoService>();

var app = builder.Build();

// Ensure database is created. For production use migrations.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.MapOpenApi();

app.UseHttpsRedirection();

// Use CORS before authorization and endpoints
app.UseCors(corsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
