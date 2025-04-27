using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Crud.DTOs;
using Crud.Services.Interfaces;
using Crud.Entities.Enums;

namespace Crud.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    [HttpPost]
    [Route("api/Recipe/{recipeId:guid}/Review/User/{userId:guid}")]
    public async Task<ActionResult<ReviewDTO>> Create(
        [FromRoute] Guid recipeId, [FromRoute] Guid userId, [FromBody] ReviewAddDTO reviewDto
    )
    {
        var review = await reviewService.Create(userId, recipeId, reviewDto);
        return Ok(review);
    }

    [HttpGet("/User/{userId:guid}")]
    public async Task<ActionResult<List<ReviewDTO>>> ReadByUser([FromRoute] Guid userId)
    {
        var reviews = await reviewService.ReadByUser(userId);
        return Ok(reviews);
    }

    [HttpPatch("{reviewId:guid}")]
    public async Task<ActionResult<ReviewDTO>> Update(
        [FromRoute] Guid reviewId, [FromBody] ReviewUpdateDTO reviewDto
    )
    {
        var review = await reviewService.Update(reviewId, reviewDto);
        return Ok(review);
    }

    [HttpDelete("{reviewId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid reviewId)
    {
        await reviewService.Delete(reviewId);
        return Ok();
    }
}

