using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Services.OperationClaimService;

public class OperationClaimManager : IOperationClaimService
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimManager(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<IList<OperationClaim>> GetList()
    {
        IList<OperationClaim> operationClaims = (await _operationClaimRepository.GetListAsync()).Items;
        return operationClaims;
    }
}