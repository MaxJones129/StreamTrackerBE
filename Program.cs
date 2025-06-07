using Microsoft.EntityFrameworkCore;
using StreamTracker.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<StreamTrackerDbContext>(options =>
        {
            // Try to get DATABASE_URL from environment variables (for Heroku)
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            string connectionString;

            if (!string.IsNullOrEmpty(databaseUrl))
            {
                // Convert Heroku DATABASE_URL (postgres://username:password@host:port/dbname)
                var uri = new Uri(databaseUrl);
                var userInfo = uri.UserInfo.Split(':');

                connectionString = $"Host={uri.Host};Port={uri.Port};Username={userInfo[0]};Password={userInfo[1]};Database={uri.AbsolutePath.TrimStart('/')};SSL Mode=Require;Trust Server Certificate=true;";
            }
            else
            {
                // Fallback to appsettings.json or development connection string
                connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            }

            options.UseNpgsql(connectionString);
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        // Add controllers and configure JSON options to handle circular references
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Configure JSON options to handle circular references
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        
        // Add Swagger for API documentation
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();

        // Apply the CORS policy to the pipeline
        app.UseCors("AllowAll");


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Show detailed exception page in development mode
        }

        app.UseAuthorization();
        app.MapControllers();

        // Initialize the database if needed (apply migrations and seed data)
        await InitializeDatabaseAsync(app);

        // Run the application
        await app.RunAsync();
    }

    // Database initialization logic (apply migrations and sample data initialization)
    private static async Task InitializeDatabaseAsync(WebApplication app)
    {
        // Create a scope to resolve services
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        // Get the DbContext
        var dbContext = services.GetRequiredService<StreamTrackerDbContext>();

        // Apply any pending migrations to the database
        await dbContext.Database.MigrateAsync();

        // Initialize sample data if needed (only if the database is empty)
        await dbContext.InitializeDatabaseAsync();
    }
}
