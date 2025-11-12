using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantsRepository> _restaurantRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantsAuthorizationService> _restaurantsAuthorizationServiceMock;

    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        _mapperMock = new Mock<IMapper>();
        _restaurantsAuthorizationServiceMock = new Mock<IRestaurantsAuthorizationService>();

        _handler = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _restaurantRepositoryMock.Object,
            _restaurantsAuthorizationServiceMock.Object);
    }


    [Fact()]
    public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
    {
        // Arrange

        var restaurantId = 1;
        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId,
            Name = "Updated Restaurant",
            Description = "Updated Description",
            HasDelivery = true
        };

        var restaurant = new Restaurant
        {
            Id = restaurantId,
            Name = "Old Restaurant",
            Description = "Old Description"
        };

        _restaurantRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
            .ReturnsAsync(restaurant);

        _restaurantsAuthorizationServiceMock.Setup(a => a.Authorize(restaurant, ResourceOperation.Update))
            .Returns(true);

        // Act

        await _handler.Handle(command, CancellationToken.None);

        // Assert

        _restaurantRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);
    }

    [Fact()]
    public async Task Handle_WithNonExisting_Restaurant_ShouldThrowNotFoundException()
    {
        // Arrange
        var restaurantId = 2;
        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId
        };

        _restaurantRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
            .ReturnsAsync((Restaurant?)null);

        // act
            
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id: {restaurantId} doesn't exist");
    }

    [Fact()]
    public async Task Handle_WithUnAuthorizedUser_ShouldThrowForbidenException()
    {
        // Arrange
        var restaurantId = 3;
        var command = new UpdateRestaurantCommand
        {
            Id = restaurantId
        };

        var existingRestaurant = new Restaurant
        {
            Id = restaurantId
        };

        _restaurantRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
            .ReturnsAsync(existingRestaurant);

        _restaurantsAuthorizationServiceMock.Setup(a => a.Authorize(existingRestaurant, ResourceOperation.Update))
            .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert

        await act.Should().ThrowAsync<ForbiddenException>();
    }
}