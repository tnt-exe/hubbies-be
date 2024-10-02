namespace Hubbies.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when a request is invalid, status code 400.
/// </summary>
/// <param name="message">The error message</param>
public class BadRequestException(string message)
    : HttpException(message, HttpStatusCode.BadRequest)
{
}
