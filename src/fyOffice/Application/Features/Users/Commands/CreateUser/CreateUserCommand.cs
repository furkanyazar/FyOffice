using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UserOperationClaimService;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<CreatedUserDto>, ISecuredRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string[] Roles => new[] { Admin };

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IUserOperationClaimService _userOperationClaimService;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper,
                                        UserBusinessRules userBusinessRules,
                                        IUserOperationClaimService userOperationClaimService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _userOperationClaimService = userOperationClaimService;
        }

        public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserEmailCanNotBeDuplicatedWhenInserted(request.Email);

            HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new()
            {
                AuthenticatorType = AuthenticatorType.None,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            User createdUser = await _userRepository.AddAsync(newUser);
            await _userOperationClaimService.AddClaimToUser(createdUser.Id);
            CreatedUserDto createdUserDto = _mapper.Map<CreatedUserDto>(createdUser);
            return createdUserDto;
        }
    }
}