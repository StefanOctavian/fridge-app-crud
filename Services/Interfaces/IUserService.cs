using Crud.DTOs;
using Crud.Entities;

namespace Crud.Services.Interfaces;

public interface IUserService 
{
    Task<UserDTO> Read(Guid id);
    Task<UserDTO> Read(string email);
    Task<List<FridgeIngredientDTO>> ReadFridge(Guid userId);
    Task<UserDTO> Update(Guid id, UserUpdateDTO userUpdate);
    Task<List<FridgeIngredientDTO>> PutFridge(
        Guid userId,
        List<FridgePutIngredientDTO> ingredients
    );
    Task<List<FridgeIngredientDTO>> UpdateFridge(
        Guid userId,
        List<FridgeIngredientDeltaDTO> ingredientsDelta
    );
}