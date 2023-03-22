namespace Application.Services.UserOperationClaimService;

public interface IUserOperationClaimService
{
    public Task AddClaimToUser(int userId);
}