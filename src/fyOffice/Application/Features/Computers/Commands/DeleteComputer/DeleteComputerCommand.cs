using Application.Features.Computers.Dtos;
using Application.Features.Computers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Computers.Commands.DeleteComputer;

public class DeleteComputerCommand : IRequest<DeletedComputerDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin };

    public class DeleteComputerCommandHandler : IRequestHandler<DeleteComputerCommand, DeletedComputerDto>
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IMapper _mapper;
        private readonly ComputerBusinessRules _computerBusinessRules;

        public DeleteComputerCommandHandler(IComputerRepository computerRepository, IMapper mapper,
                                            ComputerBusinessRules computerBusinessRules)
        {
            _computerRepository = computerRepository;
            _mapper = mapper;
            _computerBusinessRules = computerBusinessRules;
        }

        public async Task<DeletedComputerDto> Handle(DeleteComputerCommand request, CancellationToken cancellationToken)
        {
            Computer? computerToCheck = await _computerRepository.GetAsync(c => c.Id == request.Id);

            _computerBusinessRules.ComputerIdShouldExistWhenSelected(computerToCheck);
            await _computerBusinessRules.ComputerShouldNotHasEmployeeWhenDeleted(request.Id);

            Computer deletedComputer = await _computerRepository.DeleteAsync(computerToCheck);
            DeletedComputerDto deletedComputerDto = _mapper.Map<DeletedComputerDto>(deletedComputer);
            return deletedComputerDto;
        }
    }
}