using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Endpoints;

public static class EventEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        GetEvent.MapEndpoint(app);
        CreateEvent.MapEndpoint(app);
    }
}
