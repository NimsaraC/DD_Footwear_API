using AutoMapper;
using DD_Footwear.DTOs;
using DD_Footwear.Models;

namespace DD_Footwear.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
