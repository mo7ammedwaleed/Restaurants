using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.AspNetCore.Authorization.Policy;
using Restaurants.API.Tests;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Moq;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurants.Domain.Entities;
using System.Net.Http.Json;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();
    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                    _ => _restaurantsRepositoryMock.Object));
            });
        });
    }

    [Fact()]
    public async Task GetAll_ForValidRequest_ShouldReturns200Ok()
    {
        // Arrange

        var client = _factory.CreateClient();

        // Act

        var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");

        // Assert

        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact()]
    public async Task GetAll_ForInValidRequest_ShouldReturns400BadRequest()
    {
        // Arrange

        var client = _factory.CreateClient();

        // Act

        var result = await client.GetAsync("/api/restaurants");

        // Assert

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact()]
    public async Task GetById_ForNonExistingId_ShouldRetutn404NotFound()
    {
        // Arrange
        var id = 9999;

        _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);

        var client = _factory.CreateClient();

        // Act

        var result = await client.GetAsync($"/api/restaurants/{id}");

        // Assert

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact()]
    public async Task GetById_ForExistingId_ShouldRetutn200OK()
    {
        // Arrange
        var id = 99;

        var restaurant = new Restaurant()
        {
            Id = id,
            Name = "Test Restaurant",
            Description = "Test Description"
        };

        _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(restaurant);

        var client = _factory.CreateClient();

        // Act

        var result = await client.GetAsync($"/api/restaurants/{id}");
        var restaurantDTO = await result.Content.ReadFromJsonAsync<RestaurantDTO>();
        // Assert

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDTO.Should().NotBeNull();
        restaurantDTO.Name.Should().Be("Test Restaurant");
        restaurantDTO.Description.Should().Be("Test Description");
    }
}