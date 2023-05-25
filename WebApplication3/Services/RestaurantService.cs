using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMapper _mapper;
        private readonly RestaurantDbContext _dbContext;
        public RestaurantService(IMapper mapper, RestaurantDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
               .Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDtos;
        }

        public int CreateNew(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }
        public bool DeleteById (int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
                if (restaurant == null)
            {
                return false;
            }

            _dbContext .SaveChanges();
            return true;
        }
    }
}
