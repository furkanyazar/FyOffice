using Application.Features.Employees.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Employees.Models;

public class EmployeeListModel : BasePageableModel<EmployeeListDto>
{
}