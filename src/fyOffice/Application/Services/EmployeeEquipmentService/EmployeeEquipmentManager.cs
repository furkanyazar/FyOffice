using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.EmployeeEquipmentService;

public class EmployeeEquipmentManager : IEmployeeEquipmentService
{
    private readonly IEmployeeEquipmentRepository _employeeEquipmentRepository;

    public EmployeeEquipmentManager(IEmployeeEquipmentRepository employeeEquipmentRepository)
    {
        _employeeEquipmentRepository = employeeEquipmentRepository;
    }

    public IList<EmployeeEquipment> GetListByEquipmentId(int equipmentId)
    {
        IList<EmployeeEquipment> employeeEquipments =
            _employeeEquipmentRepository.GetList(c => c.EquipmentId == equipmentId).Items;
        return employeeEquipments;
    }

    public async Task<IList<EmployeeEquipment>> GetListByEquipmentIdAsync(int equipmentId)
    {
        IList<EmployeeEquipment> employeeEquipments =
            (await _employeeEquipmentRepository.GetListAsync(c => c.EquipmentId == equipmentId)).Items;
        return employeeEquipments;
    }

    public async Task UpdateEmployeeEquipments(int employeeId, List<int> equipments)
    {
        IList<EmployeeEquipment> employeeEquipments =
            (await _employeeEquipmentRepository.GetListAsync(c => c.EmployeeId == employeeId)).Items;

        foreach (var employeeEquipment in employeeEquipments)
            await _employeeEquipmentRepository.DeleteAsync(employeeEquipment);

        foreach (var equipment in equipments)
        {
            EmployeeEquipment newEmployeeEquipment = new() { EmployeeId = employeeId, EquipmentId = equipment };
            await _employeeEquipmentRepository.AddAsync(newEmployeeEquipment);
        }
    }

    public async Task UpdateEquipmentEmployees(int equipmentId, List<int> employees)
    {
        IList<EmployeeEquipment> employeeEquipments =
            (await _employeeEquipmentRepository.GetListAsync(c => c.EquipmentId == equipmentId)).Items;

        foreach (var employeeEquipment in employeeEquipments)
            await _employeeEquipmentRepository.DeleteAsync(employeeEquipment);

        foreach (var employee in employees)
        {
            EmployeeEquipment newEmployeeEquipment = new() { EmployeeId = employee, EquipmentId = equipmentId };
            await _employeeEquipmentRepository.AddAsync(newEmployeeEquipment);
        }
    }
}