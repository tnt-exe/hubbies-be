using FluentValidation.Results;

namespace Hubbies.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when a request is invalid, status code 422.
/// </summary>
public class ValidationException : HttpException, IHasErrors
{
    public IDictionary<string, string[]> Errors { get; }

    private ValidationException()
        : base("One or more validation failures have occurred.",
            HttpStatusCode.UnprocessableEntity)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(IDictionary<string, string[]> errors)
        : this()
    {
        Errors = errors;
    }
}
