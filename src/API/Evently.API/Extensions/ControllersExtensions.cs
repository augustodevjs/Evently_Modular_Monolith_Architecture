namespace Evently.API.Extensions;

internal static class ControllersExtensions
{
    internal static IServiceCollection AddAndConfigureControllers(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
        });

        return services;
    }
    
    public static void UseDocumentation(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        app.UseSwagger();
        app.UseSwaggerUI();   
        app.ApplyMigration();
    }
}
