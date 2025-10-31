using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastrucutre.Authorization.Requirements;

public class MinimamAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}
