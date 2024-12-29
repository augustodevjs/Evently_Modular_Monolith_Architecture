using MediatR;

namespace Evently.Modules.Events.Application.UseCases.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IRequest<EventResponse?>;
