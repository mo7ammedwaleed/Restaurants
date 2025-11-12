using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Infrastrucutre.Authorization.Requirements.Tests;

public class CreatedMultipleRestaurantsRequirementHandlerTests
{
    [Fact()]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
    {
        // Arrange

        var currentUser = new CurrentUser("1", "test@test.com", [],null,null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new ()
            {
                OwnerId = currentUser.Id
            },
            new ()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = "2"
            }
        };

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock.Setup(rr => rr.GetAllAsync()).ReturnsAsync(restaurants);

        var reqierment = new CreatedMultipleRestaurantsRequirement(2);
        var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantsRepositoryMock.Object,userContextMock.Object);

        var context = new AuthorizationHandlerContext([reqierment], null, null);

        // Act

        await handler.HandleAsync(context);

        // Assert

        context.HasSucceeded.Should().BeTrue();
    }

    [Fact()]
    public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
    {
        // Arrange

        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(uc => uc.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new ()
            {
                OwnerId = currentUser.Id
            },
            new()
            {
                OwnerId = "2"
            }
        };

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock.Setup(rr => rr.GetAllAsync()).ReturnsAsync(restaurants);

        var reqierment = new CreatedMultipleRestaurantsRequirement(2);
        var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantsRepositoryMock.Object, userContextMock.Object);

        var context = new AuthorizationHandlerContext([reqierment], null, null);

        // Act

        await handler.HandleAsync(context);

        // Assert

        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeTrue();
    }
}