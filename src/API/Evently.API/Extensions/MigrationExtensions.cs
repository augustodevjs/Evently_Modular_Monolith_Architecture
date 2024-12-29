using Microsoft.EntityFrameworkCore;
using Evently.Modules.Events.Infrastructure.Database;

namespace Evently.API.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigration(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        ApplyMigration<EventsDbContext>(scope);
    }
    
    private static void ApplyMigration<TDbContext>(IServiceScope scope) where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        context.Database.Migrate();
    }
}
