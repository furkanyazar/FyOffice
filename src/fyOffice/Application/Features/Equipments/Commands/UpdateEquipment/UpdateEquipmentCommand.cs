using Application.Features.Equipments.Dtos;
using Application.Features.Equipments.Rules;
using Application.Services.EmployeeEquipmentService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Equipments.Commands.UpdateEquipment;

public class UpdateEquipmentCommand : IRequest<UpdatedEquipmentDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public short UnitsInStock { get; set; }
    public List<int> Employees { get; set; }

    public string[] Roles => new[] { Admin };

    public class UpdateEquipmentCommandHandler : IRequestHandler<UpdateEquipmentCommand, UpdatedEquipmentDto>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;
        private readonly EquipmentBusinessRules _equipmentBusinessRules;
        private readonly IEmployeeEquipmentService _employeeEquipmentService;

        public UpdateEquipmentCommandHandler(IEquipmentRepository equipmentRepository, IMapper mapper,
                                             EquipmentBusinessRules equipmentBusinessRules,
                                             IEmployeeEquipmentService employeeEquipmentService)
        {
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
            _equipmentBusinessRules = equipmentBusinessRules;
            _employeeEquipmentService = employeeEquipmentService;
        }

        public async Task<UpdatedEquipmentDto> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
        {
            Equipment? equipmentToCheck = await _equipmentRepository.GetAsync(c => c.Id == request.Id);

            _equipmentBusinessRules.EquipmentIdShouldExistWhenSelected(equipmentToCheck);
            await _equipmentBusinessRules.EquipmentNameCanNotBeDuplicatedWhenUpdated(request.Id, request.Name);
            await _equipmentBusinessRules
                        .EquipmentUnitsInStockShouldNotLessThanUsedWhenUpdated(request.Id, request.UnitsInStock);

            await _employeeEquipmentService.UpdateEquipmentEmployees(request.Id, request.Employees);

            equipmentToCheck.Name = request.Name;
            equipmentToCheck.UnitsInStock = request.UnitsInStock;

            Equipment updatedEquipment = await _equipmentRepository.UpdateAsync(equipmentToCheck);
            UpdatedEquipmentDto updatedEquipmentDto = _mapper.Map<UpdatedEquipmentDto>(updatedEquipment);
            return updatedEquipmentDto;
        }
    }
}