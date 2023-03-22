using Core.Application.Dtos;

namespace Application.Features.EmployeeEquipments.Dtos;

public class EmployeeEquipmentListDto : IDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EquipmentId { get; set; }
}