using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Crud.DTOs;
using Crud.Services.Interfaces;

namespace Crud.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/")]
public class ActivationTokenController(IActivationTokenService activationTokenService) : ControllerBase
{

    [HttpGet]
    [Route("/api/User/ActivationToken/{token}")]
    public async Task<ActionResult<UserDTO>> Read([FromRoute] string token)
        => Ok(await activationTokenService.Read(token));

    [HttpPost]
    [Route("/api/User/{userId:guid}/ActivationToken")]
    public async Task<ActionResult> Create(
        [FromRoute] Guid userId,
        [FromBody] ActivationTokenDTO activationTokenDTO
    )
    {
        await activationTokenService.Create(userId, activationTokenDTO);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromRoute] string token)
    {
        await activationTokenService.Delete(token);
        return Ok();
    }
}