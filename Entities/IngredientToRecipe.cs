using Crud.Entities.Enums;

namespace Crud.Entities;

public abstract class IngredientQuantity : BaseEntity
{
    public required Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    public required double Quantity { get; set; } = 0.0;
    public required Unit Unit { get; set; } = Unit.Gram;
}

public class IngredientToRecipe : IngredientQuantity
{
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
}