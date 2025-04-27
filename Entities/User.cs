using Crud.Entities.Enums;

namespace Crud.Entities;

public class User : BaseEntity 
{ 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public UserRole Role { get; set; } = UserRole.User;
    public ICollection<IngredientToUser> Fridge { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
}
