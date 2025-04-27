namespace Crud.DTOs;

public record ActivationTokenDTO(
    string Token,
    DateTime ExpirationDate
);
    