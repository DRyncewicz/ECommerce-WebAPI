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
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(IMapper mapper, RestaurantDbContext dbContext, ILogger<RestaurantService> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
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
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
                if (restaurant == null)
            {
                return false;
            }

            _dbContext.Remove(restaurant);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Update(RestaurantUpdateDto dto, int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault (r => r.Id == id);

            if (restaurant == null)
            {
                return false;
            }
            _dbContext.Entry(restaurant).CurrentValues.SetValues(dto);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
