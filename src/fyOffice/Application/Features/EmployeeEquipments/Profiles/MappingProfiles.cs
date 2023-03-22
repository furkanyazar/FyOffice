using Application.Features.EmployeeEquipments.Dtos;
using Application.Features.EmployeeEquipments.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.EmployeeEquipments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<EmployeeEquipment, EmployeeEquipmentListDto>().ReverseMap();
        CreateMap<IPaginate<EmployeeEquipment>, EmployeeEquipmentListModel>().ReverseMap();
    }
}