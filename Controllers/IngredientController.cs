using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Crud.DTOs;
using Crud.Services.Interfaces;
using Crud.Entities.Enums;

namespace Crud.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/")]
public class IngredientController(IIngredientService ingredientService) : ControllerBase
{

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IngredientDTO>> Read([FromRoute] Guid id)
        => Ok(new { data = await ingredientService.Read(id) });

    [HttpGet]
    public async Task<ActionResult<IngredientDTO>> Read([FromQuery] string? name)
    {
        if (name is null)
            return Ok(new { data = await ingredientService.Read() });

        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name cannot be empty");
        return Ok(new { data = await ingredientService.Read(name) });
    }

    [HttpPost]
    public async Task<ActionResult<IngredientDTO>> Create([FromBody] IngredientAddDTO ingredient)
    {
        return Ok(new { data = await ingredientService.Create(ingredient) });
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<IngredientDTO>> Update([FromRoute] Guid id, [FromBody] IngredientUpdateDTO ingredient)
    {
        return Ok(new { data = await ingredientService.Update(id, ingredient) });
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await ingredientService.Delete(id);
        return Ok(new {});
    }
}