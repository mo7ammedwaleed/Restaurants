using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handel_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        //arrange

        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();

        mapperMock
            .Setup(x => x.Map<Restaurant>(command))
            .Returns(restaurant);

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock
            .Setup(x => x.Create(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);

        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object
            , mapperMock.Object
            , restaurantsRepositoryMock.Object
            ,userContextMock.Object);

        //act

        var result = await commandHandler.Handle(command, CancellationToken.None);

        //assert

        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantsRepositoryMock.Verify(x => x.Create(restaurant), Times.Once);
    }
}