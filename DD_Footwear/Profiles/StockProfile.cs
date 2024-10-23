using AutoMapper;
using DD_Footwear.DTOs;
using DD_Footwear.Models;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockDto>()
            .ForMember(dest => dest.Stocks, opt => opt.MapFrom(src => src.StockItems ))
            .ReverseMap();

        CreateMap<StockItems, StockItemsDto>().ReverseMap();
    }
}
