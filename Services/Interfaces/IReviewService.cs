using Crud.DTOs;

namespace Crud.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewDTO> Read(Guid userId, Guid recipeId);
    Task<ReviewDTO> Create(Guid userId, Guid recipeId, ReviewAddDTO reviewDto);
    Task<List<ReviewDTO>> ReadByUser(Guid userId);
    Task<ReviewDTO> Update(Guid reviewId, ReviewUpdateDTO reviewDto);
    Task Delete(Guid reviewId);
}