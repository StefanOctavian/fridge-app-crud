using Microsoft.AspNetCore.Mvc;

using Crud.DTOs;
using Crud.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Crud.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/")]
public class RecipeController(IRecipeService recipeService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] RecipeCreateDTO recipe)
    {
        await recipeService.Create(recipe);
        return Ok(new {});
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RecipeDTO>> Read([FromRoute] Guid id)
        => Ok(new { data = await recipeService.Read(id) });

    [HttpGet]
    public async Task<ActionResult<List<RecipePreviewDTO>>> Read(
        [FromQuery] bool withIngredients = false,
        [FromQuery] bool withReviews = false
    ) => Ok(new { data = await recipeService.Read(withIngredients, withReviews) });

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<RecipeDTO>> Update(
        [FromRoute] Guid id, [FromBody] RecipeUpdateDTO recipeUpdates
    ) => Ok(new { data = await recipeService.Update(id, recipeUpdates) });

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await recipeService.Delete(id);
        return Ok(new {});
    }
}