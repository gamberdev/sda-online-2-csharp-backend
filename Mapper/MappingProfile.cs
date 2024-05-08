using AutoMapper;
using DB_EF.Models;
using ecommerce.EntityFramework.Table;
using ecommerce.Models;

namespace ecommerce.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserModel, UserViewModel>();
        CreateMap<User, UserViewModel>();
    }
}
