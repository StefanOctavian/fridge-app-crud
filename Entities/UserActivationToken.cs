namespace Crud.Entities;

public class UserActivationToken : BaseEntity
{
    public required Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public required string Token { get; set; }
    public required DateTime ExpirationDate { get; set; }
}