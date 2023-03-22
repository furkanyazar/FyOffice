using Core.Application.Dtos;

namespace Application.Features.Employees.Dtos;

public class CreatedEmployeeDto : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? DateOfBirth { get; set; }
}