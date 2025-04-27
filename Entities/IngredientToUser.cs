namespace Crud.Entities;

public class IngredientToUser : IngredientQuantity
{
    public required Guid UserId { get; set; }
    public User User { get; set; } = null!;
}