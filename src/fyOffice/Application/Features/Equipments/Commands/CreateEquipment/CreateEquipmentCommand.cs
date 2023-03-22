using Application.Features.Equipments.Dtos;
using Application.Features.Equipments.Dtos;
using Application.Features.Equipments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Equipments.Commands.CreateEquipment;

public class CreateEquipmentCommand : IRequest<CreatedEquipmentDto>, ISecuredRequest
{
    public string Name { get; set; }
    public short UnitsInStock { get; set; }

    public string[] Roles => new[] { Admin };

    public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, CreatedEquipmentDto>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;
        private readonly EquipmentBusinessRules _equipmentBusinessRules;

        public CreateEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IMapper mapper,
                                             EquipmentBusinessRules equipmentBusinessRules)
        {
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
            _equipmentBusinessRules = equipmentBusinessRules;
        }

        public async Task<CreatedEquipmentDto> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            await _equipmentBusinessRules.EquipmentNameCanNotBeDuplicatedWhenInserted(request.Name);

            Equipment mappedEquipment = _mapper.Map<Equipment>(request);
            Equipment createdEquipment = await _equipmentRepository.AddAsync(mappedEquipment);
            CreatedEquipmentDto createdEquipmentDto = _mapper.Map<CreatedEquipmentDto>(createdEquipment);
            return createdEquipmentDto;
        }
    }
}