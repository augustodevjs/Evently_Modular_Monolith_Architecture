using Microsoft.EntityFrameworkCore;
using Evently.Modules.Events.Infrastructure.Database;

namespace Evently.API.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigration(this IApplicationBuilder app)
    {
        // Creates a scoped lifetime for retrieving services and ensure that the scope object is properly
        // disposed of when the method completes, realising resources.
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        ApplyMigration<EventsDbContext>(scope);
    }
    
    private static void ApplyMigration<TDbContext>(IServiceScope scope) where TDbContext : DbContext
    {
        // Get instance of TDbContext registered in my serviceProvider
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        context.Database.Migrate();
    }
}
