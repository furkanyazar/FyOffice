using Application.Features.Equipments.Dtos;
using Application.Services.EmployeeEquipmentService;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Equipments.Profiles;

public class EquipmentDtoUnitsInRemainingResolver : IValueResolver<Equipment, EquipmentDto, short>
{
    private readonly IEmployeeEquipmentService _employeeEquipmentService;

    public EquipmentDtoUnitsInRemainingResolver(IEmployeeEquipmentService employeeEquipmentService)
    {
        _employeeEquipmentService = employeeEquipmentService;
    }

    public short Resolve(Equipment source, EquipmentDto destination, short destMember, ResolutionContext context)
    {
        IList<EmployeeEquipment> employeeEquipments = _employeeEquipmentService.GetListByEquipmentId(source.Id);
        return Convert.ToInt16(source.UnitsInStock - employeeEquipments.Count);
    }
}