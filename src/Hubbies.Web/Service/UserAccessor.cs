using System.Security.Claims;

namespace Hubbies.Web.Service;

public class UserAccessor(IHttpContextAccessor accessor) : IUser
{
    public string? Id
        => accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? IpAddress
        => accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
}
