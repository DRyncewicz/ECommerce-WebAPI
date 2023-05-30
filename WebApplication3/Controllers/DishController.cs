using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }
        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId, [FromBody]CreateDishDto dto)
        {
            var newDishId = _service.Create(restaurantId, dto);
            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}", null);
        }
        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = _service.GetById(restaurantId, dishId);
            return Ok(dish);
            
        }
        [HttpGet]
        public ActionResult<DishDto> GetAll([FromRoute] int restaurantId)
        {
            var dishes = _service.GetAll(restaurantId);
            return Ok(dishes);
        }
        [HttpDelete("{dishId}")]
        public ActionResult DeleteById(int restaurantId, int dishId)
        {
            _service.DeleteById(restaurantId, dishId);
            return Ok();
        }
        [HttpDelete]
        public ActionResult DeleteAll(int restaurantId)
        {
            _service.Delete(restaurantId);
            return NoContent();
        }
    }
}
