using Core.Application.Dtos;

namespace Application.Features.Computers.Dtos;

public class UpdatedComputerDto : IDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }
    public bool HasLicence { get; set; }
}