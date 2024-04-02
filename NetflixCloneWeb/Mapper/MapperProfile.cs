using AutoMapper;
using NetflixCloneWeb.Dtos;
using NetflixCloneWeb.Models;

namespace NetflixCloneWeb.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Video, VideoDto>().ReverseMap();
            CreateMap<MyList, MyListDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserLogin, UserLoginDto>().ReverseMap();
        }
    }
}
