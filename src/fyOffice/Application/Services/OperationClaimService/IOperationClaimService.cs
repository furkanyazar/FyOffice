using Core.Security.Entities;

namespace Application.Services.OperationClaimService;

public interface IOperationClaimService
{
    public Task<IList<OperationClaim>> GetList();
}