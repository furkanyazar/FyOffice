using Application.Features.Employees.Dtos;
using Application.Features.Employees.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommand : IRequest<CreatedEmployeeDto>, ISecuredRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? DateOfBirth { get; set; }

    public string[] Roles => new[] { Admin };

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreatedEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly EmployeeBusinessRules _employeeBusinessRules;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper,
                                            EmployeeBusinessRules employeeBusinessRules)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<CreatedEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeBusinessRules.EmployeePhoneNumberCanNotBeDuplicatedWhenInserted(request.PhoneNumber);

            Employee mappedEmployee = _mapper.Map<Employee>(request);
            Employee createdEmployee = await _employeeRepository.AddAsync(mappedEmployee);
            CreatedEmployeeDto createdEmployeeDto = _mapper.Map<CreatedEmployeeDto>(createdEmployee);
            return createdEmployeeDto;
        }
    }
}