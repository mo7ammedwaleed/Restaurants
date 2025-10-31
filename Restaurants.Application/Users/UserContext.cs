using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users;

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
        var nationality = user.FindFirst(e => e.Type == "Nationality")?.Value;
        var dateOfBirthString = user.FindFirst(e => e.Type == "DateOfBirth")?.Value;
        var dateOfBirth = dateOfBirthString == null ? (DateOnly?)null : DateOnly.Parse(dateOfBirthString);


        return new CurrentUser(userId, email, roles, dateOfBirthString, dateOfBirth);

    }
}
