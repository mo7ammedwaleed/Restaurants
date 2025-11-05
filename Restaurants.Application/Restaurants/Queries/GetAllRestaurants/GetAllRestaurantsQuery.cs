using MediatR;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDTO>>
{
    public string? SearchPhrase { get; set; }
}
