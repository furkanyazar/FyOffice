using Application.Features.Auths.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;

    public UserBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void UserIdShouldExistWhenSelected(User? user)
    {
        if (user is null)
            throw new BusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
    {
        User? result = await _userRepository.GetAsync(c => c.Email == email);
        if (result is not null)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserEmailCanNotBeDuplicatedWhenUpdated(int id, string email)
    {
        User? result = await _userRepository.GetAsync(c => c.Id != id && c.Email == email, enableTracking: false);
        if (result is not null)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }
}