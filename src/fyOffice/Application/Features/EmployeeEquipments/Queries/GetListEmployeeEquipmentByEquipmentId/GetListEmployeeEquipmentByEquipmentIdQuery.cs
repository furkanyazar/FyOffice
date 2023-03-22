using Application.Features.EmployeeEquipments.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.EmployeeEquipments.Queries.GetListEmployeeEquipmentByEquipmentId;

public class GetListEmployeeEquipmentByEquipmentIdQuery : IRequest<EmployeeEquipmentListModel>, ISecuredRequest
{
    public int EquipmentId { get; set; }
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };

    public class GetListEmployeeEquipmentByEquipmentIdQueryHandler :
        IRequestHandler<GetListEmployeeEquipmentByEquipmentIdQuery, EmployeeEquipmentListModel>
    {
        private readonly IEmployeeEquipmentRepository _employeeEquipmentRepository;
        private readonly IMapper _mapper;

        public GetListEmployeeEquipmentByEquipmentIdQueryHandler(IEmployeeEquipmentRepository employeeEquipmentRepository,
                                                                 IMapper mapper)
        {
            _employeeEquipmentRepository = employeeEquipmentRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeEquipmentListModel> Handle(GetListEmployeeEquipmentByEquipmentIdQuery request,
                                                       CancellationToken cancellationToken)
        {
            IPaginate<EmployeeEquipment> employeeEquipments =
                await _employeeEquipmentRepository.GetListAsync(c => c.EquipmentId == request.EquipmentId,
                                                                index: request.PageRequest.Page,
                                                                size: request.PageRequest.PageSize);
            EmployeeEquipmentListModel mappedEmployeeEquipmentListModel =
                _mapper.Map<EmployeeEquipmentListModel>(employeeEquipments);
            return mappedEmployeeEquipmentListModel;
        }
    }
}