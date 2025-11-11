using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.DTOs.Tests;

public class RestaurantProfileTests
{
    [Fact()]
    public void CreateMap_ForRestaurantDTO_MapsCorrectly()
    {
        // arrange

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
        });

        var mapper = configuration.CreateMapper();

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "A test restaurant",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@test.com",
            ContactNumber = "1234567890",
            Address = new Address
            {
                Street = "123 Test St",
                City = "Test City",
                PostalCode = "12345"
            }
        };

        // act

        var restaurantDTO = mapper.Map<RestaurantDTO>(restaurant);

        // assert

        restaurantDTO.Should().NotBeNull();
        restaurantDTO.Id.Should().Be(restaurant.Id);
        restaurantDTO.Name.Should().Be(restaurant.Name);
        restaurantDTO.Description.Should().Be(restaurant.Description);
        restaurantDTO.Category.Should().Be(restaurant.Category);
        restaurantDTO.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDTO.City.Should().Be(restaurant.Address.City);
        restaurantDTO.Street.Should().Be(restaurant.Address.Street);
        restaurantDTO.PostalCode.Should().Be(restaurant.Address.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
        });

        var mapper = configuration.CreateMapper();

        var Command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "A test restaurant",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@test.com",
            ContactNumber = "1234567890",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = "12345"
        };

        // act

        var restaurantCommand = mapper.Map<Restaurant>(Command);

        // assert

        restaurantCommand.Should().NotBeNull();
        restaurantCommand.Name.Should().Be(Command.Name);
        restaurantCommand.Description.Should().Be(Command.Description);
        restaurantCommand.Category.Should().Be(Command.Category);
        restaurantCommand.HasDelivery.Should().Be(Command.HasDelivery);
        restaurantCommand.ContactEmail.Should().Be(Command.ContactEmail);
        restaurantCommand.ContactNumber.Should().Be(Command.ContactNumber);
        restaurantCommand.Address.Should().NotBeNull();
        restaurantCommand.Address.City.Should().Be(Command.City);
        restaurantCommand.Address.Street.Should().Be(Command.Street);
        restaurantCommand.Address.PostalCode.Should().Be(Command.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
        });

        var mapper = configuration.CreateMapper();

        var Command = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "A test restaurant",
            HasDelivery = true
        };

        // act

        var restaurantCommand = mapper.Map<Restaurant>(Command);

        // assert

        restaurantCommand.Should().NotBeNull();
        restaurantCommand.Id.Should().Be(Command.Id);
        restaurantCommand.Name.Should().Be(Command.Name);
        restaurantCommand.Description.Should().Be(Command.Description);
        restaurantCommand.HasDelivery.Should().Be(Command.HasDelivery);
    }
}