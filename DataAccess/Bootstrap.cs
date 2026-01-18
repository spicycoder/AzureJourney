using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DataAccess;

public static class Bootstrap
{
    public static void Initialize(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ProductsDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    public static async Task Migrate(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
