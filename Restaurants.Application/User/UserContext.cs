using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.User;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user == null)
        {
            throw new InvalidOperationException("User context is not present");
        }
        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }
        var userId = user.FindFirst(e => e.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(e => e.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(e => e.Type == ClaimTypes.Role)!.Select(e => e.Value);

        return new CurrentUser(userId, email, roles);

    }
}
