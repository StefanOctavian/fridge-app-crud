using Crud.DTOs;

namespace Crud.Services.Interfaces;

public interface IActivationTokenService
{
    Task Create(Guid userId, ActivationTokenDTO activationTokenDTO);
    Task<UserDTO> Read(string token);
    Task Delete(string token);
}
