using Crud.Database;
using Crud.Services.Interfaces;
using Crud.DTOs;
using Crud.Entities;
using Microsoft.EntityFrameworkCore;
using Crud.Errors;

namespace Crud.Services.Implementations;

public class ReviewService(FridgeAppDatabaseContext dbContext) : IReviewService
{
    public static ReviewDTO ReviewToDTO(Review review)
    {
        return new ReviewDTO(
            Id: review.Id,
            UserId: review.UserId,
            RecipeId: review.RecipeId,
            Rating: review.Rating,
            Comment: review.Comment,
            CreatedAt: review.CreatedAt
        );
    }

    public async Task<ReviewDTO> Read(Guid userId, Guid recipeId)
    {
        var review = await dbContext.Reviews
            .FirstOrDefaultAsync(r => r.UserId == userId && r.RecipeId == recipeId)
            ?? throw new NotFoundException("Review not found");

        return ReviewToDTO(review);
    }

    public async Task<List<ReviewDTO>> ReadByUser(Guid userId)
    {
        var reviews = await dbContext.Reviews
            .Where(r => r.UserId == userId)
            .ToListAsync();

        return [..reviews.Select(ReviewToDTO)];
    }

    public async Task<ReviewDTO> Create(Guid userId, Guid recipeId, ReviewAddDTO reviewDto)
    {
        var recipe = await dbContext.Recipes.FindAsync(recipeId)
            ?? throw new NotFoundException("Recipe not found");

        var review = new Review
        {
            UserId = userId,
            RecipeId = recipeId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment
        };

        await dbContext.Reviews.AddAsync(review);
        await dbContext.SaveChangesAsync();

        return ReviewToDTO(review);
    }

    public async Task<ReviewDTO> Update(Guid reviewId, ReviewUpdateDTO reviewDto)
    {
        var review = await dbContext.Reviews.FindAsync(reviewId)
            ?? throw new NotFoundException("Review not found");

        review.Rating = reviewDto.Rating ?? review.Rating;
        review.Comment = reviewDto.Comment ?? review.Comment;
        review.UpdatedAt = DateTime.UtcNow;

        dbContext.Reviews.Update(review);
        await dbContext.SaveChangesAsync();

        return ReviewToDTO(review);
    }

    public async Task Delete(Guid reviewId)
    {
        var review = await dbContext.Reviews.FindAsync(reviewId)
            ?? throw new NotFoundException("Review not found");

        dbContext.Reviews.Remove(review);
        await dbContext.SaveChangesAsync();
    }

}