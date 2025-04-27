using System.ComponentModel.DataAnnotations;

namespace Crud.DTOs;

public record ReviewDTO(
    Guid Id,
    Guid UserId,
    Guid RecipeId,
    [Range(1, 5)] int Rating,
    string Comment,
    DateTime CreatedAt
);

public record ReviewAddDTO(
    [Range(1, 5)] int Rating,
    string Comment
);

public record ReviewUpdateDTO(
    [Range(1, 5)] int? Rating,
    string? Comment
);