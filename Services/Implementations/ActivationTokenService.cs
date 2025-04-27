using Crud.Database;
using Crud.Services.Interfaces;
using Crud.DTOs;
using Crud.Entities;
using Crud.Errors;
using Microsoft.EntityFrameworkCore;

namespace Crud.Services.Implementations;

public class ActivationTokenService(FridgeAppDatabaseContext databaseContext) : IActivationTokenService
{
    public async Task Create(Guid userId, ActivationTokenDTO activationTokenDTO)
    {
        var activationToken = new UserActivationToken
        {
            UserId = userId,
            Token = activationTokenDTO.Token,
            ExpirationDate = activationTokenDTO.ExpirationDate
        };

        databaseContext.UserActivationTokens.Add(activationToken);
        await databaseContext.SaveChangesAsync();
    }

    public async Task<UserDTO> Read(string token)
    {
        var activationToken = await databaseContext.UserActivationTokens
            .FirstOrDefaultAsync(at => at.Token == token && at.ExpirationDate > DateTime.UtcNow)
            ?? throw new NotFoundException($"Activation token {token} not found or expired.");

        var user = await databaseContext.Users
            .FirstOrDefaultAsync(u => u.Id == activationToken.UserId)
            ?? throw new NotFoundException($"User with id {activationToken.UserId} not found.");

        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsVerified = user.IsVerified,
            Role = user.Role,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Password = user.Password,
            Salt = user.Salt
        };
    }

    public async Task Delete(string token)
    {
        var activationToken = await databaseContext.UserActivationTokens
            .FirstOrDefaultAsync(at => at.Token == token)
            ?? throw new NotFoundException($"Activation token {token} not found.");

        databaseContext.UserActivationTokens.Remove(activationToken);
        await databaseContext.SaveChangesAsync();
    }
}
    