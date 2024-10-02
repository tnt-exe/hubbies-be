using Microsoft.AspNetCore.Identity;

namespace Hubbies.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when an entity is conflict as it already existed, status code 409.
/// </summary>
public class ConflictException : HttpException, IHasErrors
{
    public IDictionary<string, string[]> Errors { get; }

    private ConflictException()
        : base("One or more validation failures have occurred.",
        HttpStatusCode.Conflict)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ConflictException(IEnumerable<IdentityError> errors)
        : this()
    {
        Errors = errors
            .GroupBy(e => e.Code, e => e.Description)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
