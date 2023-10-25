using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extension
{
    public static class HostExtention
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Migration database associated with context {typeof(TContext).Name}");

                    context.Database.Migrate();

                    logger.LogInformation($"Migration database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occured while migrating the database used on context {typeof(TContext).Name}");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }
            return host;
        }
    }
}
