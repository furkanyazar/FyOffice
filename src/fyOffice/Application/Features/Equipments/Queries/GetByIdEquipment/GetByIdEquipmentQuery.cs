using Application.Features.Equipments.Dtos;
using Application.Features.Equipments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Equipments.Queries.GetByIdEquipment;

public class GetByIdEquipmentQuery : IRequest<EquipmentDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin };

    public class GetByIdEquipmentQueryHandler : IRequestHandler<GetByIdEquipmentQuery, EquipmentDto>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;
        private readonly EquipmentBusinessRules _equipmentBusinessRules;

        public GetByIdEquipmentQueryHandler(IEquipmentRepository equipmentRepository, IMapper mapper,
                                            EquipmentBusinessRules equipmentBusinessRules)
        {
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
            _equipmentBusinessRules = equipmentBusinessRules;
        }

        public async Task<EquipmentDto> Handle(GetByIdEquipmentQuery request, CancellationToken cancellationToken)
        {
            Equipment? equipment = await _equipmentRepository.GetAsync(c => c.Id == request.Id);

            _equipmentBusinessRules.EquipmentIdShouldExistWhenSelected(equipment);

            EquipmentDto mappedEquipment = _mapper.Map<EquipmentDto>(equipment);
            return mappedEquipment;
        }
    }
}