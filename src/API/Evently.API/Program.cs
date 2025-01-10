using Evently.API.Utils;
using Evently.API.Extensions;
using Evently.API.Middleware;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Events.Infrastructure;
using Evently.Modules.Ticketing.Infrastructure;
using Evently.Modules.Users.Infrastructure;

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
    .AddUsersModule(builder.Configuration)
    .AddEventsModule(builder.Configuration)
    .AddTicketingModule(builder.Configuration);

WebApplication app = builder.Build();

app.UseDocumentation();
app.MapEndpoints();
app.UseApplicationHealthCheck();
app.ConfigureRequestLogging();  
app.UseExceptionHandler();

app.Run();
