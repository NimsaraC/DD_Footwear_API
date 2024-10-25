using AutoMapper;
using DD_Footwear.DTOs;
using DD_Footwear.Models;

namespace DD_Footwear.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
