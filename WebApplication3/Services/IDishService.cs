using WebApplication3.Entities;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IDishService
    {

        public int Create(int restaurantId, CreateDishDto dto);
        public DishDto GetById(int restaurantId, int dishId);
        public IEnumerable<DishDto> GetAll(int restaurantId);
        public void DeleteById(int restaurantId, int dishId);
        public void Delete(int restaurantId);

    }
}
