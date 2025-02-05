using AutoMapper;
using CommonServicesLib.DTOs;
using CommonServicesLib.Models;

namespace ShoppingCartService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
        }
    }
}
