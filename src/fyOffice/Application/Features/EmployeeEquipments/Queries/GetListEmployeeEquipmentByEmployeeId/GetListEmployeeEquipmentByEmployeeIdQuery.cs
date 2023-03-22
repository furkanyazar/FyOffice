using Application.Features.EmployeeEquipments.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.EmployeeEquipments.Queries.GetListEmployeeEquipmentByEmployeeId;

public class GetListEmployeeEquipmentByEmployeeIdQuery : IRequest<EmployeeEquipmentListModel>, ISecuredRequest
{
    public int EmployeeId { get; set; }
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };

    public class GetListEmployeeEquipmentByEmployeeIdQueryHandler :
        IRequestHandler<GetListEmployeeEquipmentByEmployeeIdQuery, EmployeeEquipmentListModel>
    {
        private readonly IEmployeeEquipmentRepository _employeeEquipmentRepository;
        private readonly IMapper _mapper;

        public GetListEmployeeEquipmentByEmployeeIdQueryHandler(IEmployeeEquipmentRepository employeeEquipmentRepository,
                                                                IMapper mapper)
        {
            _employeeEquipmentRepository = employeeEquipmentRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeEquipmentListModel> Handle(GetListEmployeeEquipmentByEmployeeIdQuery request,
                                                             CancellationToken cancellationToken)
        {
            IPaginate<EmployeeEquipment> employeeEquipments =
                await _employeeEquipmentRepository.GetListAsync(c => c.EmployeeId == request.EmployeeId,
                                                                index: request.PageRequest.Page,
                                                                size: request.PageRequest.PageSize);
            EmployeeEquipmentListModel mappedEmployeeEquipmentListModel =
                _mapper.Map<EmployeeEquipmentListModel>(employeeEquipments);
            return mappedEmployeeEquipmentListModel;
        }
    }
}