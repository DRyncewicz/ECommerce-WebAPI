using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : Controller
    {

        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _service.GetAll();
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id)
        {
            var restaurantDto = _service.GetById(id);
            if (restaurantDto == null)
            {
                return NotFound();
            }
            return Ok(restaurantDto);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Id = _service.CreateNew(dto);
            return Created($"/api/restaurant/{Id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant([FromRoute]int id)
        {
            bool isDeleted = _service.DeleteById(id);    
            return isDeleted? Ok() : NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant([FromBody]RestaurantUpdateDto dto,[FromRoute] int id)
        {
            bool isDeleted = _service.Update(dto, id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
