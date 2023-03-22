using Application.Features.Equipments.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Equipments.Queries.GetListEquipment;

public class GetListEquipmentQuery : IRequest<EquipmentListModel>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };

    public class GetListEquipmentQueryHandler : IRequestHandler<GetListEquipmentQuery, EquipmentListModel>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;

        public GetListEquipmentQueryHandler(IEquipmentRepository equipmentRepository, IMapper mapper)
        {
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
        }

        public async Task<EquipmentListModel> Handle(GetListEquipmentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Equipment> equipments =
                await _equipmentRepository.GetListAsync(index: request.PageRequest.Page,
                                                        size: request.PageRequest.PageSize);
            EquipmentListModel mappedEquipmentListModel = _mapper.Map<EquipmentListModel>(equipments);
            return mappedEquipmentListModel;
        }
    }
}