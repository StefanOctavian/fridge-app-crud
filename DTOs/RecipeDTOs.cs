using Crud.Entities.Enums;

namespace Crud.DTOs;

public class RecipePreviewDTO 
{
    public Guid Id { get; set; }
    public required string Name { get; set; } 
    public required string ImageUrl { get; set; }
    public required string Description { get; set; }
    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public RecipeDifficulty Difficulty { get; set; }
};

public class RecipeDTO : RecipePreviewDTO
{
    public required List<IngredientQuantityDTO> Ingredients { get; set; } 
    public required string Body { get; set; }
    public List<ReviewDTO> Reviews { get; set; } = [];
}

public record RecipeCreateDTO(
    string Title,
    string Description,
    string ImageUrl,
    int CookingTime,
    RecipeDifficulty Difficulty,
    string Body,
    List<IngredientQuantityAddDTO> Ingredients,
    string? VideoUrl = null,
    int Servings = 1
);