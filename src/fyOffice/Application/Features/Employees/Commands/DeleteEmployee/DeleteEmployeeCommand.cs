using Application.Features.Employees.Dtos;
using Application.Features.Employees.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<DeletedEmployeeDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin };

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, DeletedEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly EmployeeBusinessRules _employeeBusinessRules;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper,
                                            EmployeeBusinessRules employeeBusinessRules)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<DeletedEmployeeDto> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee? employeeToCheck = await _employeeRepository.GetAsync(c => c.Id == request.Id);

            _employeeBusinessRules.EmployeeIdShouldExistWhenSelected(employeeToCheck);
            await _employeeBusinessRules.EmployeeShouldNotHasComputerWhenDeleted(request.Id);

            Employee deletedEmployee = await _employeeRepository.DeleteAsync(employeeToCheck);
            DeletedEmployeeDto deletedEmployeeDto = _mapper.Map<DeletedEmployeeDto>(deletedEmployee);
            return deletedEmployeeDto;
        }
    }
}