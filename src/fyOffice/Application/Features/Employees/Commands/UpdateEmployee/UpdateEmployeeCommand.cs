using Application.Features.Employees.Dtos;
using Application.Features.Employees.Rules;
using Application.Services.EmployeeEquipmentService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System.Globalization;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest<UpdatedEmployeeDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? DateOfBirth { get; set; }
    public List<int> Equipments { get; set; }

    public string[] Roles => new[] { Admin };

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdatedEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly EmployeeBusinessRules _employeeBusinessRules;
        private readonly IEmployeeEquipmentService _employeeEquipmentService;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper,
                                            EmployeeBusinessRules employeeBusinessRules,
                                            IEmployeeEquipmentService employeeEquipmentService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _employeeBusinessRules = employeeBusinessRules;
            _employeeEquipmentService = employeeEquipmentService;
        }

        public async Task<UpdatedEmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee? employeeToCheck = await _employeeRepository.GetAsync(c => c.Id == request.Id);

            _employeeBusinessRules.EmployeeIdShouldExistWhenSelected(employeeToCheck);
            await _employeeBusinessRules.EmployeePhoneNumberCanNotBeDuplicatedWhenUpdated(request.Id, request.PhoneNumber);

            await _employeeEquipmentService.UpdateEmployeeEquipments(request.Id, request.Equipments);

            employeeToCheck.FirstName = request.FirstName;
            employeeToCheck.LastName = request.LastName;
            employeeToCheck.PhoneNumber = !string.IsNullOrEmpty(request.PhoneNumber) ? request.PhoneNumber : null;
            employeeToCheck.DateOfBirth = !string.IsNullOrEmpty(request.DateOfBirth)
                ? Convert.ToDateTime(DateTime.ParseExact(request.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                : null;

            Employee updatedEmployee = await _employeeRepository.UpdateAsync(employeeToCheck);
            UpdatedEmployeeDto updatedEmployeeDto = _mapper.Map<UpdatedEmployeeDto>(updatedEmployee);
            return updatedEmployeeDto;
        }
    }
}