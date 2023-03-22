using Application.Features.Equipments.Dtos;
using Application.Services.EmployeeEquipmentService;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Equipments.Profiles;

public class UpdatedEquipmentDtoUnitsInRemainingResolver : IValueResolver<Equipment, UpdatedEquipmentDto, short>
{
    private readonly IEmployeeEquipmentService _employeeEquipmentService;

    public UpdatedEquipmentDtoUnitsInRemainingResolver(IEmployeeEquipmentService employeeEquipmentService)
    {
        _employeeEquipmentService = employeeEquipmentService;
    }

    public short Resolve(Equipment source, UpdatedEquipmentDto destination, short destMember, ResolutionContext context)
    {
        IList<EmployeeEquipment> employeeEquipments = _employeeEquipmentService.GetListByEquipmentId(source.Id);
        return Convert.ToInt16(source.UnitsInStock - employeeEquipments.Count);
    }
}