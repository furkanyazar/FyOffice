using Application.Features.Equipments.Commands.CreateEquipment;
using Application.Features.Equipments.Commands.DeleteEquipment;
using Application.Features.Equipments.Commands.UpdateEquipment;
using Application.Features.Equipments.Dtos;
using Application.Features.Equipments.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Equipments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Equipment, EquipmentDto>()
            .ForMember(dest => dest.UnitsInRemaining, opt => opt.MapFrom<EquipmentDtoUnitsInRemainingResolver>())
            .ReverseMap();
        CreateMap<Equipment, EquipmentListDto>()
            .ForMember(dest => dest.UnitsInRemaining, opt => opt.MapFrom<EquipmentListDtoUnitsInRemainingResolver>())
            .ReverseMap();
        CreateMap<IPaginate<Equipment>, EquipmentListModel>().ReverseMap();
        CreateMap<Equipment, DeletedEquipmentDto>().ReverseMap();
        CreateMap<Equipment, UpdatedEquipmentDto>()
            .ForMember(dest => dest.UnitsInRemaining, opt => opt.MapFrom<UpdatedEquipmentDtoUnitsInRemainingResolver>())
            .ReverseMap();
        CreateMap<Equipment, CreateEquipmentCommand>().ReverseMap();
        CreateMap<Equipment, CreatedEquipmentDto>()
            .ForMember(dest => dest.UnitsInRemaining, opt => opt.MapFrom<CreatedEquipmentDtoUnitsInRemainingResolver>())
            .ReverseMap();
    }
}