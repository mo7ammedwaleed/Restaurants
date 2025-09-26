using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO?>
{
    public async Task<RestaurantDTO?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting restaurant {RestaurantId}",request.Id);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        var restaurantDTO = mapper.Map<RestaurantDTO?>(restaurant);
        return restaurantDTO;
    }
}
