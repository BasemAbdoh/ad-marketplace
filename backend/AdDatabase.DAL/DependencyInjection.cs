using AdDatabase.BLL.Interfaces;
using AdDatabase.DAL.Data;
using AdDatabase.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdDatabase.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection")
            ?? configuration["DATABASE_URL"]
            ?? throw new InvalidOperationException("Database connection string is missing.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (connection.StartsWith("postgres", StringComparison.OrdinalIgnoreCase))
                options.UseNpgsql(ToNpgsqlConnectionString(connection), x => x.EnableRetryOnFailure());
            else if (connection.Contains("Host=", StringComparison.OrdinalIgnoreCase))
                options.UseNpgsql(connection, x => x.EnableRetryOnFailure());
            else
                options.UseSqlServer(connection, x => x.EnableRetryOnFailure());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAdRepository, AdRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }

    private static string ToNpgsqlConnectionString(string value)
    {
        var uri = new Uri(value);
        var credentials = uri.UserInfo.Split(':', 2);
        var database = uri.AbsolutePath.TrimStart('/');
        return $"Host={uri.Host};Port={(uri.IsDefaultPort ? 5432 : uri.Port)};Database={Uri.UnescapeDataString(database)};Username={Uri.UnescapeDataString(credentials[0])};Password={Uri.UnescapeDataString(credentials.ElementAtOrDefault(1) ?? string.Empty)};SSL Mode=Require;Trust Server Certificate=true";
    }
}
