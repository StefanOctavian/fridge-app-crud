using Crud.Entities.Enums;

namespace Crud.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string Salt { get; set; }
    public required string Email { get; set; }
    public required UserRole Role { get; set; }
    public required bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateUserDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string Salt { get; set; }
    public required string Email { get; set; }
    public required UserRole Role { get; set; }
}

public class UserUpdateDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public string? Email { get; set; }
    public UserRole? Role { get; set; }
    public bool? IsVerified { get; set; }
}