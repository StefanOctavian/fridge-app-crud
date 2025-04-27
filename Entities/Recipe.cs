using Crud.Entities.Enums;

namespace Crud.Entities;
public class Recipe : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int CookingTime { get; set; } // in minutes
    public int Servings { get; set; } // number of servings
    public RecipeDifficulty Difficulty { get; set; } = RecipeDifficulty.Beginner;
    public ICollection<IngredientToRecipe> Ingredients { get; set; } = [];
    public string? VideoUrl { get; set; } = null;
    public string Body { get; set; } = string.Empty;
    public ICollection<Review> Reviews { get; set; } = [];
}