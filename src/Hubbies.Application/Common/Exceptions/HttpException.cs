namespace Hubbies.Application.Common.Exceptions;

public class HttpException(
    string? message,
    HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public interface IHasErrors
{
    IDictionary<string, string[]> Errors { get; }
}
