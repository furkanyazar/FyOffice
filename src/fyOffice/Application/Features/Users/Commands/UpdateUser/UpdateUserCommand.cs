using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Core.Security.Hashing;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<UpdatedUserDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }

    public string[] Roles => new[] { Admin };

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper,
                                        UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<UpdatedUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? userToCheck = await _userRepository.GetAsync(c => c.Id == request.Id);
                
            _userBusinessRules.UserIdShouldExistWhenSelected(userToCheck);
            await _userBusinessRules.UserEmailCanNotBeDuplicatedWhenUpdated(request.Id, request.Email);

            userToCheck.FirstName = request.FirstName;
            userToCheck.LastName = request.LastName;
            userToCheck.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Password))
            {
                HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userToCheck.PasswordHash = passwordHash;
                userToCheck.PasswordSalt = passwordSalt;
            }

            User updatedUser = await _userRepository.UpdateAsync(userToCheck);
            UpdatedUserDto updatedUserDto = _mapper.Map<UpdatedUserDto>(updatedUser);
            return updatedUserDto;
        }
    }
}