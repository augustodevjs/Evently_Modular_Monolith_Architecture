using Evently.API.Utils;
using Evently.API.Extensions;
using Evently.API.Middleware;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Events.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureApplicationLogging();

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddAndConfigureControllers()
    .AddApplication(ModulesReference.Assemblies)
    .AddInfrastructure(builder.Configuration);

builder.Configuration.AddModuleConfiguration(ModulesReference.ModuleNames);

builder.Services
    .ConfigureApplicationHealthChecks(builder.Configuration)
    .AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

app.UseDocumentation();
app.MapEndpoints();
app.UseApplicationHealthCheck();
app.ConfigureRequestLogging();  
app.UseExceptionHandler();

app.Run();
