using Core.Application.Dtos;

namespace Application.Features.Equipments.Dtos;

public class EquipmentListDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public short UnitsInStock { get; set; }
    public short UnitsInRemaining { get; set; }
}