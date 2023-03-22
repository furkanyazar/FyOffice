using Application.Features.Equipments.Constants;
using Application.Services.EmployeeEquipmentService;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Equipments.Rules;

public class EquipmentBusinessRules : BaseBusinessRules
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IEmployeeEquipmentService _employeeEquipmentService;

    public EquipmentBusinessRules(IEquipmentRepository equipmentRepository,
                                  IEmployeeEquipmentService employeeEquipmentService)
    {
        _equipmentRepository = equipmentRepository;
        _employeeEquipmentService = employeeEquipmentService;
    }

    public void EquipmentIdShouldExistWhenSelected(Equipment? equipment)
    {
        if (equipment is null)
            throw new BusinessException(EquipmentMessages.EquipmentNotExists);
    }

    public async Task EquipmentNameCanNotBeDuplicatedWhenInserted(string name)
    {
        Equipment? result = await _equipmentRepository.GetAsync(c => c.Name == name);
        if (result is not null)
            throw new BusinessException(EquipmentMessages.EquipmentNameAlreadyExists);
    }

    public async Task EquipmentNameCanNotBeDuplicatedWhenUpdated(int id, string name)
    {
        Equipment? result = await _equipmentRepository.GetAsync(c => c.Id != id && c.Name == name, enableTracking: false);
        if (result is not null)
            throw new BusinessException(EquipmentMessages.EquipmentNameAlreadyExists);
    }

    public async Task EquipmentShouldNotBeUsedWhenDeleted(int id)
    {
        IList<EmployeeEquipment> result = await _employeeEquipmentService.GetListByEquipmentIdAsync(id);
        if (result.Count > 0)
            throw new BusinessException(EquipmentMessages.EquipmentIsUsed);
    }

    public async Task EquipmentUnitsInStockShouldNotLessThanUsedWhenUpdated(int id, short unitsInStock)
    {
        IList<EmployeeEquipment> result = await _employeeEquipmentService.GetListByEquipmentIdAsync(id);
        if (unitsInStock < result.Count)
            throw new BusinessException(EquipmentMessages.EquipmentIsMissing);
    }
}