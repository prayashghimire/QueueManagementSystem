using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueMs.Responses;
using QueueMS.Appilication.DTOs.Token;
using QueueMS.Appilication.Interfaces;
using System.Security.Claims;

namespace QueueMs.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<IActionResult> TakeToken([FromBody] CreateToken dto)
    {
        var user = GetCurrentUserId();

        var result = await _tokenService.TakeTokenAsync(dto.ServiceId, user);

        return ApiResponses.Ok("Token Taken successfully", result);

    }

    [HttpGet("{tokenId:int}")]
    public async Task<IActionResult>GetToken(int tokenId)
    {
        var result = await _tokenService.GetTokenAsync(tokenId);
        if(result == null)
        {
            return ApiResponses.NotFound("Token Not Found", result);
        }
        return ApiResponses.Ok("Token Found", result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTokens()
    {
        var user = GetCurrentUserId();
        var result = await _tokenService.GetCustomerTokenAsync(user);

        return ApiResponses.Ok("Got User Tokens", result);
    }

    [HttpDelete("{tokenId:int}")]
    public async Task<IActionResult> CancelToken(int tokenId)
    {
        var user = GetCurrentUserId();
        await _tokenService.CancelTokenAsync(tokenId, user);

        return ApiResponses.Ok("Token Cancelled Successfully", tokenId);
    }



    private int GetCurrentUserId()
    {
        var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(string.IsNullOrEmpty(user))
            throw new UnauthorizedAccessException("User id was not found");

        return int.Parse(user);

    }
}
