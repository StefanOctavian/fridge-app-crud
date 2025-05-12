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
    [Route("/api/User/[controller]/{token}")]
    public async Task<ActionResult<UserDTO>> Read([FromRoute] string token)
        => Ok(new { data = await activationTokenService.Read(token) });

    [HttpPost]
    [Route("/api/User/{userId:guid}/[controller]")]
    public async Task<ActionResult> Create(
        [FromRoute] Guid userId,
        [FromBody] ActivationTokenDTO activationTokenDTO
    )
    {
        await activationTokenService.Create(userId, activationTokenDTO);
        return Ok(new {});
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromRoute] string token)
    {
        await activationTokenService.Delete(token);
        return Ok(new {});
    }
}