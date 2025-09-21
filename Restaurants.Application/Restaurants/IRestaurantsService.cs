using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurants();
        Task<RestaurantDTO?> GetById(int id);
        Task<int> Create(CreateRestaurantDTO entity);
    }
}