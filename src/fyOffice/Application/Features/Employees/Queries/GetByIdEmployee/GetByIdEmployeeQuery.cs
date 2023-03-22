using Application.Features.Employees.Dtos;
using Application.Features.Employees.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Employees.Queries.GetByIdEmployee;

public class GetByIdEmployeeQuery : IRequest<EmployeeDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin };

    public class GetByIdEmployeeQueryHandler : IRequestHandler<GetByIdEmployeeQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly EmployeeBusinessRules _employeeBusinessRules;

        public GetByIdEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper,
                                           EmployeeBusinessRules employeeBusinessRules)
        {
            _employeeRepository = employeeRepository;
            _employeeBusinessRules = employeeBusinessRules;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetByIdEmployeeQuery request, CancellationToken cancellationToken)
        {
            Employee? employee = await _employeeRepository.GetAsync(c => c.Id == request.Id,
                                                                    include: c => c.Include(c => c.Computer));

            _employeeBusinessRules.EmployeeIdShouldExistWhenSelected(employee);

            EmployeeDto mappedEmployeeDto = _mapper.Map<EmployeeDto>(employee);
            return mappedEmployeeDto;
        }
    }
}