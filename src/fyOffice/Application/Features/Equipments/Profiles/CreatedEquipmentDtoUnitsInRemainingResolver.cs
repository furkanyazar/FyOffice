using Application.Features.Equipments.Dtos;
using Application.Services.EmployeeEquipmentService;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Equipments.Profiles;

public class CreatedEquipmentDtoUnitsInRemainingResolver : IValueResolver<Equipment, CreatedEquipmentDto, short>
{
    private readonly IEmployeeEquipmentService _employeeEquipmentService;

    public CreatedEquipmentDtoUnitsInRemainingResolver(IEmployeeEquipmentService employeeEquipmentService)
    {
        _employeeEquipmentService = employeeEquipmentService;
    }

    public short Resolve(Equipment source, CreatedEquipmentDto destination, short destMember, ResolutionContext context)
    {
        IList<EmployeeEquipment> employeeEquipments = _employeeEquipmentService.GetListByEquipmentId(source.Id);
        return Convert.ToInt16(source.UnitsInStock - employeeEquipments.Count);
    }
}