using Application.Features.Equipments.Dtos;
using Application.Features.Equipments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Equipments.Commands.DeleteEquipment;

public class DeleteEquipmentCommand : IRequest<DeletedEquipmentDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin };

    public class DeleteEquipmentCommandHandler : IRequestHandler<DeleteEquipmentCommand, DeletedEquipmentDto>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;
        private readonly EquipmentBusinessRules _equipmentBusinessRules;

        public DeleteEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IMapper mapper,
                                             EquipmentBusinessRules equipmentBusinessRules)
        {
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
            _equipmentBusinessRules = equipmentBusinessRules;
        }

        public async Task<DeletedEquipmentDto> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
        {
            Equipment? equipmentToCheck = await _equipmentRepository.GetAsync(c => c.Id == request.Id);

            _equipmentBusinessRules.EquipmentIdShouldExistWhenSelected(equipmentToCheck);
            await _equipmentBusinessRules.EquipmentShouldNotBeUsedWhenDeleted(request.Id);

            Equipment deletedEquipment = await _equipmentRepository.DeleteAsync(equipmentToCheck);
            DeletedEquipmentDto deletedEquipmentDto = _mapper.Map<DeletedEquipmentDto>(deletedEquipment);
            return deletedEquipmentDto;
        }
    }
}