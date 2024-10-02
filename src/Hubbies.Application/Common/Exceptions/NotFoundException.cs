namespace Hubbies.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when an entity is not found, status code 404.
/// </summary>
/// <param name="entityName">The name of the entity</param>
/// <param name="key">The key of the entity</param>
public class NotFoundException(string entityName, object key)
    : HttpException($"Entity '{entityName}' with identifier ({key}) was not found.", HttpStatusCode.NotFound)
{
}
