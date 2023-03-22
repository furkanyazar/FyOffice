using Application.Features.Computers.Commands.CreateComputer;
using Application.Features.Computers.Commands.DeleteComputer;
using Application.Features.Computers.Commands.UpdateComputer;
using Application.Features.Computers.Dtos;
using Application.Features.Computers.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Computers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Computer, ComputerDto>()
            .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.Employee.LastName))
            .ReverseMap();
        CreateMap<Computer, ComputerListDto>()
            .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.Employee.LastName))
            .ForMember(dest => dest.HasLicence, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LicenceKey)))
            .ReverseMap();
        CreateMap<IPaginate<Computer>, ComputerListModel>().ReverseMap();
        CreateMap<Computer, DeletedComputerDto>().ReverseMap();
        CreateMap<Computer, CreateComputerCommand>()
            .ReverseMap()
            .ForMember(dest => dest.Processor,
                       opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Processor) ? src.Processor : null))
            .ForMember(dest => dest.Memory,
                       opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Memory) ? src.Memory : null))
            .ForMember(dest => dest.LicenceKey,
                       opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LicenceKey) ? src.LicenceKey : null))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Note) ? src.Note : null))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId == 0 ? null : src.EmployeeId));
        CreateMap<Computer, CreatedComputerDto>()
            .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.Employee.LastName))
            .ForMember(dest => dest.HasLicence, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LicenceKey)))
            .ReverseMap();
        CreateMap<Computer, UpdatedComputerDto>()
            .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.Employee.LastName))
            .ForMember(dest => dest.HasLicence, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LicenceKey)))
            .ReverseMap();
    }
}