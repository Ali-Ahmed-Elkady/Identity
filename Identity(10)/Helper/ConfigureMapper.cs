using AutoMapper;
using Identity_10_.Models.Entities;
using Identity_10_.Services.User.DTO;

namespace Identity_10_.Helper
{
    public class ConfigureMapper : Profile
    {
       public ConfigureMapper()
        {
            #region User Mappings
            CreateMap<AppUser,UserDto>().ReverseMap();
            CreateMap<AppUser,RegisterDto>().ReverseMap();
            #endregion
        }
    }
}
