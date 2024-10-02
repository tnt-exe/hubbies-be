namespace Hubbies.Web.Common;

public class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
#pragma warning disable CS8604 // Possible null reference argument.
        => value == null
        ? null
        : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
#pragma warning restore CS8604 // Possible null reference argument.
}
