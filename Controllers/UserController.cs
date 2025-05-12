using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Crud.DTOs;
using Crud.Services.Interfaces;
using Crud.Responses;

namespace Crud.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/")]
public class UserController(IUserService userService, IReviewService reviewService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserDTO?>> Create([FromBody] CreateUserDTO user)
        => Ok(new { data = await userService.Create(user) });

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDTO?>> Read([FromRoute] Guid id)
        => Ok(new { data = await userService.Read(id) });

    [HttpGet]
    public async Task<ActionResult<UserDTO?>> Read([FromQuery] string email)
        => Ok(new { data = await userService.Read(email) });

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<UserDTO?>> UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateDTO userUpdate)
        => Ok(new { data = await userService.Update(id, userUpdate) });

    [HttpGet("/{id:guid}/Fridge")]
    public async Task<ActionResult<List<FridgeIngredientDTO>>> ReadFridge([FromRoute] Guid id)
        => Ok(new { data = await userService.ReadFridge(id) });

    [HttpGet("/{id:guid}/Reviews")]
    public async Task<ActionResult<List<ReviewDTO>>> GetReviews([FromRoute] Guid id)
        => Ok(new { data = await reviewService.ReadByUser(id) });

    [HttpPut("/{id:guid}/Fridge")]
    public async Task<ActionResult<List<FridgeIngredientDTO>>> PutFridge(
        [FromRoute] Guid id,
        [FromBody] List<FridgePutIngredientDTO> ingredients
    ) => Ok(new { data = await userService.PutFridge(id, ingredients) });

    [HttpPatch("{id:guid}/Fridge")]
    public async Task<ActionResult<List<FridgeIngredientDTO>>> UpdateFridge(
        [FromRoute] Guid id,
        [FromBody] List<FridgeIngredientDeltaDTO> ingredientsDelta
    ) => Ok(new { data = await userService.UpdateFridge(id, ingredientsDelta) });
}