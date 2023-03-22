using Application.Features.Employees.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Employees.Queries.GetListEmployee;

public class GetListEmployeeQuery : IRequest<EmployeeListModel>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };

    public class GetListEmployeeQueryHandler : IRequestHandler<GetListEmployeeQuery, EmployeeListModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetListEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeListModel> Handle(GetListEmployeeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Employee> employees =
                await _employeeRepository.GetListAsync(index: request.PageRequest.Page,
                                                       size: request.PageRequest.PageSize);
            EmployeeListModel mappedEmployeeListModel = _mapper.Map<EmployeeListModel>(employees);
            return mappedEmployeeListModel;
        }
    }
}