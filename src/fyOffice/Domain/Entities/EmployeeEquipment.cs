using Core.Persistence.Repositories;

namespace Domain.Entities;

public class EmployeeEquipment : Entity
{
    public int EmployeeId { get; set; }
    public int EquipmentId { get; set; }

    public virtual Employee Employee { get; set; }
    public virtual Equipment Equipment { get; set; }

    public EmployeeEquipment()
    {
    }

    public EmployeeEquipment(int id, int employeeId, int equipmentId) : base(id)
    {
        EmployeeId = employeeId;
        EquipmentId = equipmentId;
    }
}