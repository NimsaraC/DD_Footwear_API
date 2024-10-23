using AutoMapper;
using DD_Footwear.DTOs;
using DD_Footwear.Models;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
