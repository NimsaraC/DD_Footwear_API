using AutoMapper;
using DD_Footwear.DTOs;
using DD_Footwear.Models;

namespace DD_Footwear.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<CreateOrderDto, Order>();
        }
    }

}
