using Application.Services.OperationClaimService;
using Application.Services.Repositories;
using Core.Security.Entities;

namespace Application.Services.UserOperationClaimService;

public class UserOperationClaimManager : IUserOperationClaimService
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IOperationClaimService _operationClaimService;

    public UserOperationClaimManager(IUserOperationClaimRepository userOperationClaimRepository,
                                     IOperationClaimService operationClaimService)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _operationClaimService = operationClaimService;
    }

    public async Task AddClaimToUser(int userId)
    {
        IList<OperationClaim> operationClaims = await _operationClaimService.GetList();
        UserOperationClaim userOperationClaim = new()
        {
            OperationClaimId = operationClaims.FirstOrDefault().Id,
            UserId = userId,
        };

        await _userOperationClaimRepository.AddAsync(userOperationClaim);
    }
}