using Core.Application.Dtos;

namespace Application.Features.Computers.Dtos;

public class ComputerDto : IDto
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public string Brand { get; set; }
    public string? Processor { get; set; }
    public string? Memory { get; set; }
    public string? LicenceKey { get; set; }
    public string? Note { get; set; }
    public string? EmployeeFirstName { get; set; }
    public string? EmployeeLastName { get; set; }
}