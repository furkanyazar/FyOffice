using Application.Features.Computers.Dtos;
using Application.Features.Computers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Computers.Commands.CreateComputer;

public class CreateComputerCommand : IRequest<CreatedComputerDto>, ISecuredRequest
{
    public int? EmployeeId { get; set; }
    public string Brand { get; set; }
    public string? Processor { get; set; }
    public string? Memory { get; set; }
    public string? LicenceKey { get; set; }
    public string? Note { get; set; }

    public string[] Roles => new[] { Admin };

    public class CreateComputerCommandHandler : IRequestHandler<CreateComputerCommand, CreatedComputerDto>
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IMapper _mapper;
        private readonly ComputerBusinessRules _computerBusinessRules;

        public CreateComputerCommandHandler(IComputerRepository computerRepository, IMapper mapper,
                                            ComputerBusinessRules computerBusinessRules)
        {
            _computerRepository = computerRepository;
            _mapper = mapper;
            _computerBusinessRules = computerBusinessRules;
        }

        public async Task<CreatedComputerDto> Handle(CreateComputerCommand request, CancellationToken cancellationToken)
        {
            await _computerBusinessRules.ComputerEmployeeIdCanNotBeDuplicatedWhenInserted(request.EmployeeId);

            Computer mappedComputer = _mapper.Map<Computer>(request);
            Computer createdComputer = await _computerRepository.AddAsync(mappedComputer);
            CreatedComputerDto createdComputerDto = _mapper.Map<CreatedComputerDto>(createdComputer);
            return createdComputerDto;
        }
    }
}