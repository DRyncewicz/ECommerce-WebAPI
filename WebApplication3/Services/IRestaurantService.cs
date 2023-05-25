using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IRestaurantService
    {
        public RestaurantDto GetById(int id);
        public IEnumerable<RestaurantDto> GetAll();

        public int CreateNew(CreateRestaurantDto dto);

        public bool DeleteById(int id);

        public bool Update(RestaurantUpdateDto dto, int id);   
    }
}
