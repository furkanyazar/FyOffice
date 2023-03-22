using Application.Features.Computers.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Computers.Queries.GetListComputer;

public class GetListComputerQuery : IRequest<ComputerListModel>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };

    public class GetListComputerQueryHandler : IRequestHandler<GetListComputerQuery, ComputerListModel>
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IMapper _mapper;

        public GetListComputerQueryHandler(IComputerRepository computerRepository, IMapper mapper)
        {
            _computerRepository = computerRepository;
            _mapper = mapper;
        }

        public async Task<ComputerListModel> Handle(GetListComputerQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Computer> computers =
                await _computerRepository.GetListAsync(index: request.PageRequest.Page,
                                                       size: request.PageRequest.PageSize,
                                                       include: c => c.Include(c => c.Employee));
            ComputerListModel mappedComputerListModel = _mapper.Map<ComputerListModel>(computers);
            return mappedComputerListModel;
        }
    }
}