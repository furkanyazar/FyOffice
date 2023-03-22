using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.RefleshToken;
using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Commands.RevokeToken;
using Application.Features.Auths.Dtos;
using Core.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : BaseController
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IPAddress = GetIpAddress() };
        LoggedDto result = await Mediator.Send(loginCommand);

        if (result.RefreshToken is not null)
            SetRefreshTokenToCookie(result.RefreshToken);

        return Ok(result.CreateResponseDto());
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> RefreshToken()
    {
        RefreshTokenCommand refreshTokenCommand = new()
        {
            RefleshToken = GetRefreshTokenFromCookies(),
            IPAddress = GetIpAddress()
        };
        RefreshedTokensDto result = await Mediator.Send(refreshTokenCommand);
        SetRefreshTokenToCookie(result.RefreshToken);
        return Created("", result.AccessToken);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
    {
        RevokeTokenCommand revokeTokenCommand = new()
        {
            Token = refreshToken ?? GetRefreshTokenFromCookies(),
            IPAddress = GetIpAddress()
        };
        RevokedTokenDto result = await Mediator.Send(revokeTokenCommand);
        return Ok(result);
    }
}