using Evently.Common.Domain;

namespace Evently.Common.Application.Exceptions;

public sealed class EventlyException : Exception
{
    public Error? Error { get; }
    
    public string RequestName { get; }
    
    public EventlyException(string requestName, Error? error = default, Exception? innerException = default) 
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }
}
