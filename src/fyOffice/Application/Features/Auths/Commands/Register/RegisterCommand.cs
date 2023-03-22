using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UserOperationClaimService;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Register;

public class RegisterCommand : IRequest<RegisteredDto>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IPAddress { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserOperationClaimService _userOperationClaimService;

        public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService,
                                      AuthBusinessRules authBusinessRules,
                                      IUserOperationClaimService userOperationClaimService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _userOperationClaimService = userOperationClaimService;
        }

        public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password,
                                             out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new()
            {
                Email = request.UserForRegisterDto.Email,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                AuthenticatorType=Core.Security.Enums.AuthenticatorType.None
            };

            User createdUser = await _userRepository.AddAsync(newUser);
            await _userOperationClaimService.AddClaimToUser(createdUser.Id);
            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IPAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            RegisteredDto registeredDto = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredDto;
        }
    }
}