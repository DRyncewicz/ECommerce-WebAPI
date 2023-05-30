using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication3.Entities;
using WebApplication3.Exceptions;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class DishService : IDishService
    {
        private readonly IMapper _mapper;
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<DishService> _logger;

        public DishService(IMapper mapper, RestaurantDbContext dbContext, ILogger<DishService> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _mapper.Map<Dish>(dto);
            dish.RestaurantId = restaurantId;
            _dbContext.Dishes.Add(dish);
            _dbContext.SaveChanges();

            return dish.Id;
        }
        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId);
            if(dish is null || dish.RestaurantId != restaurant.Id)
            {
                throw new NotFoundException("Dish not found");
            }
            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
        public IEnumerable<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);
            if (dishDtos.IsNullOrEmpty())
            {
                throw new NotFoundException("Dishes not found");
            }
            return dishDtos;

        }

        public void DeleteById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null || dish.RestaurantId != restaurant.Id)
            {
                throw new NotFoundException("Dish not found");
            }
            _dbContext.Remove(dish);
            _dbContext.SaveChanges();
        }
        public void Delete(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            _dbContext.RemoveRange(restaurant.Dishes);
            _dbContext.SaveChanges();
        }
        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _dbContext
             .Restaurants
             .Include(r => r.Dishes)
             .FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            return restaurant;
        }
    }
    
}
