using Application.Features.Employees.Commands.CreateEmployee;
using Application.Features.Employees.Commands.DeleteEmployee;
using Application.Features.Employees.Commands.UpdateEmployee;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System.Globalization;

namespace Application.Features.Employees.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.DateOfBirth,
                       opt => opt.MapFrom(src => src.DateOfBirth.Value.ToString("dd.MM.yyyy")))
            .ForMember(dest => dest.ComputerBrand, opt => opt.MapFrom(src => src.Computer.Brand))
            .ForMember(dest => dest.ComputerHasLicence,
                       opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Computer.LicenceKey)))
            .ReverseMap();
        CreateMap<Employee, EmployeeListDto>()
            .ForMember(dest => dest.DateOfBirth,
                       opt => opt.MapFrom(src => src.DateOfBirth.Value.ToString("dd.MM.yyyy")))
            .ReverseMap();
        CreateMap<IPaginate<Employee>, EmployeeListModel>().ReverseMap();
        CreateMap<Employee, DeletedEmployeeDto>().ReverseMap();
        CreateMap<Employee, CreateEmployeeCommand>()
            .ReverseMap()
            .ForMember(dest => dest.PhoneNumber,
                       opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.PhoneNumber) ? src.PhoneNumber : null))
            .ForMember(dest => dest.DateOfBirth,
                       opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DateOfBirth)
                                                    ? Convert.ToDateTime(DateTime.ParseExact(src.DateOfBirth, "dd.MM.yyyy",
                                                                                             CultureInfo.InvariantCulture))
                                                    : (DateTime?)null));
        CreateMap<Employee, CreatedEmployeeDto>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Value.ToString("dd.MM.yyyy")))
            .ReverseMap();
        CreateMap<Employee, UpdatedEmployeeDto>()
             .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Value.ToString("dd.MM.yyyy")))
             .ReverseMap();
    }
}