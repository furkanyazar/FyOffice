using Application.Features.Computers.Dtos;
using Application.Features.Computers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Computers.Queries.GetByIdComputer;

public class GetByIdComputerQuery : IRequest<ComputerDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin };

    public class GetByIdComputerQueryHandler : IRequestHandler<GetByIdComputerQuery, ComputerDto>
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IMapper _mapper;
        private readonly ComputerBusinessRules _computerBusinessRules;

        public GetByIdComputerQueryHandler(IComputerRepository computerRepository, IMapper mapper,
                                           ComputerBusinessRules computerBusinessRules)
        {
            _computerRepository = computerRepository;
            _computerBusinessRules = computerBusinessRules;
            _mapper = mapper;
        }

        public async Task<ComputerDto> Handle(GetByIdComputerQuery request, CancellationToken cancellationToken)
        {
            Computer? computer = await _computerRepository.GetAsync(c => c.Id == request.Id,
                                                                    include: c => c.Include(c => c.Employee));

            _computerBusinessRules.ComputerIdShouldExistWhenSelected(computer);

            ComputerDto mappedComputerDto = _mapper.Map<ComputerDto>(computer);
            return mappedComputerDto;
        }
    }
}