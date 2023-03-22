using Domain.Entities;

namespace Application.Services.EmployeeEquipmentService;

public interface IEmployeeEquipmentService
{
    public IList<EmployeeEquipment> GetListByEquipmentId(int equipmentId);

    public Task<IList<EmployeeEquipment>> GetListByEquipmentIdAsync(int equipmentId);

    public Task UpdateEmployeeEquipments(int employeeId, List<int> equipments);

    public Task UpdateEquipmentEmployees(int equipmentId, List<int> employees);
}