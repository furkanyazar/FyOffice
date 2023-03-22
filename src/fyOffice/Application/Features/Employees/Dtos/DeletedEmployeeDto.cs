using Core.Application.Dtos;

namespace Application.Features.Employees.Dtos;

public class DeletedEmployeeDto : IDto
{
    public int Id { get; set; }
}