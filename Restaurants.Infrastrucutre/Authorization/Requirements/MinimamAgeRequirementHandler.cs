using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastrucutre.Authorization.Requirements;

public class MinimamAgeRequirementHandler(ILogger<MinimamAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimamAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimamAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("User: {Email}, Date Of Birth {dob} - Handling MinimumAgeRequirement",
            currentUser.Email,
            currentUser.DateOfBirth);

        if (currentUser.DateOfBirth == null)
        {
            logger.LogInformation("Authorization Failed - No Date of Birth");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization Succeeded");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("Authorization Failed");
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
