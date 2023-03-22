using Application.Features.Equipments.Dtos;
using Application.Services.EmployeeEquipmentService;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Equipments.Profiles;

public class EquipmentListDtoUnitsInRemainingResolver : IValueResolver<Equipment, EquipmentListDto, short>
{
    private readonly IEmployeeEquipmentService _employeeEquipmentService;

    public EquipmentListDtoUnitsInRemainingResolver(IEmployeeEquipmentService employeeEquipmentService)
    {
        _employeeEquipmentService = employeeEquipmentService;
    }

    public short Resolve(Equipment source, EquipmentListDto destination, short destMember, ResolutionContext context)
    {
        IList<EmployeeEquipment> employeeEquipments = _employeeEquipmentService.GetListByEquipmentId(source.Id);
        return Convert.ToInt16(source.UnitsInStock - employeeEquipments.Count);
    }
}