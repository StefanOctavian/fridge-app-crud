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
        => Ok(await ingredientService.Read(id));

    [HttpGet]
    public async Task<ActionResult<IngredientDTO>> Read([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name cannot be null or empty.");
        return Ok(await ingredientService.Read(name));
    }

    [HttpGet]
    public async Task<ActionResult<List<IngredientDTO>>> Read()
    {
        return Ok(await ingredientService.Read());
    }

    [HttpPost]
    public async Task<ActionResult<IngredientDTO>> Create([FromBody] IngredientAddDTO ingredient)
    {
        return Ok(await ingredientService.Create(ingredient));
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<IngredientDTO>> Update([FromRoute] Guid id, [FromBody] IngredientUpdateDTO ingredient)
    {
        return Ok(await ingredientService.Update(id, ingredient));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await ingredientService.Delete(id);
        return Ok();
    }
}