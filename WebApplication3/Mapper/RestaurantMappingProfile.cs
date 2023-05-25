using AutoMapper;
using WebApplication3.Entities;
using WebApplication3.Models;

namespace WebApplication3.Mapper
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<Restaurant, RestaurantUpdateDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(m => m.Address, c => c.MapFrom(dto => new Address() {City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));
        }
    }
}
