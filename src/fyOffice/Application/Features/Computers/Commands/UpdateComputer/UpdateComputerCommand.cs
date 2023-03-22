using Application.Features.Computers.Dtos;
using Application.Features.Computers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Computers.Commands.UpdateComputer;

public class UpdateComputerCommand : IRequest<UpdatedComputerDto>, ISecuredRequest
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public string Brand { get; set; }
    public string? Processor { get; set; }
    public string? Memory { get; set; }
    public string? LicenceKey { get; set; }
    public string? Note { get; set; }

    public string[] Roles => new[] { Admin };

    public class UpdateComputerCommandHandler : IRequestHandler<UpdateComputerCommand, UpdatedComputerDto>
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IMapper _mapper;
        private readonly ComputerBusinessRules _computerBusinessRules;

        public UpdateComputerCommandHandler(IComputerRepository computerRepository, IMapper mapper,
                                            ComputerBusinessRules computerBusinessRules)
        {
            _computerRepository = computerRepository;
            _mapper = mapper;
            _computerBusinessRules = computerBusinessRules;
        }

        public async Task<UpdatedComputerDto> Handle(UpdateComputerCommand request, CancellationToken cancellationToken)
        {
            Computer? computerToCheck = await _computerRepository.GetAsync(c => c.Id == request.Id);

            _computerBusinessRules.ComputerIdShouldExistWhenSelected(computerToCheck);
            await _computerBusinessRules.ComputerEmployeeIdCanNotBeDuplicatedWhenUpdated(request.Id, request.EmployeeId);

            computerToCheck.EmployeeId = request.EmployeeId == 0 ? null : request.EmployeeId;
            computerToCheck.Brand = request.Brand;
            computerToCheck.Processor = !string.IsNullOrEmpty(request.Processor) ? request.Processor : null;
            computerToCheck.Memory = !string.IsNullOrEmpty(request.Memory) ? request.Memory : null;
            computerToCheck.LicenceKey = !string.IsNullOrEmpty(request.LicenceKey) ? request.LicenceKey : null;
            computerToCheck.Note = !string.IsNullOrEmpty(request.Note) ? request.Note : null;

            Computer updatedComputer = await _computerRepository.UpdateAsync(computerToCheck);
            UpdatedComputerDto updatedComputerDto = _mapper.Map<UpdatedComputerDto>(updatedComputer);
            return updatedComputerDto;
        }
    }
}