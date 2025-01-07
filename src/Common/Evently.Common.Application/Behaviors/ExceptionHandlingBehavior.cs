using MediatR;
using Microsoft.Extensions.Logging;
using Evently.Common.Application.Exceptions;

namespace Evently.Common.Application.Behaviors;

internal sealed class ExceptionHandlingBehavior<TRequest, TResponse>(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception for {RequestName}", typeof(TRequest).Name);
            
            throw new EventlyException(typeof(TRequest).Name, innerException: exception);
        }
    }
}
