using AutoMapper;
using ImageBLL.Models.Auth;
using ImageBLL.Models.Favorite;
using ImageDAL.Models;

namespace ImageBLL.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ReverseMap();
        CreateMap<ResponseModelDto, User>()
            .ReverseMap();
        CreateMap<Image, FavoriteImageDto>().ForMember( x => x.ImageId,y => y.MapFrom(x => x.Id))
            .ReverseMap();
    }
}