using Crud.DTOs;

namespace Crud.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewDTO> Create(Guid userId, Guid recipeId, ReviewAddDTO reviewDto);
    Task<IEnumerable<ReviewDTO>> ReadByUser(Guid userId);
    Task<ReviewDTO> Update(Guid reviewId, ReviewUpdateDTO reviewDto);
    Task Delete(Guid reviewId);
}