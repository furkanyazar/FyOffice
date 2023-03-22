using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserListDto>().ReverseMap();
        CreateMap<IPaginate<User>, UserListModel>().ReverseMap();
        CreateMap<User, DeletedUserDto>().ReverseMap();
        CreateMap<User, CreatedUserDto>().ReverseMap();
        CreateMap<User, UpdatedUserDto>().ReverseMap();
    }
}