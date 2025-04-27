using Microsoft.EntityFrameworkCore;

using Crud.Database;
using Crud.DTOs;
using Crud.Entities;
using Crud.Errors;
using Crud.Services.Interfaces;
using Crud.Entities.Enums;

namespace Crud.Services.Implementations;

public class UserService(FridgeAppDatabaseContext dbContext) : IUserService
{
    private static UserDTO UserToDTO(User user)
    {
        return new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Salt = user.Salt,
            Email = user.Email,
            Role = user.Role
        };
    }
    public async Task<UserDTO> Read(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id) ?? throw CommonErrors.UserNotFound;
        return UserToDTO(user);
    }

    public async Task<UserDTO> Read(string email)
    {
        var user = await dbContext.Users.Where(u => u.Email == email).FirstAsync()
            ?? throw CommonErrors.UserNotFound;
        return UserToDTO(user);
    }

    public async Task<UserDTO> Update(Guid id, UserUpdateDTO userUpdate)
    {
        var user = await dbContext.Users.FindAsync(id) ?? throw CommonErrors.UserNotFound;

        user.FirstName = userUpdate.FirstName ?? user.FirstName;
        user.LastName = userUpdate.LastName ?? user.LastName;
        user.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return UserToDTO(user);
    }

    private FridgeIngredientDTO FridgeIngredientToDTO(IngredientToUser ingredientToUser)
    {
        Ingredient ingredient = ingredientToUser.Ingredient ??
            dbContext.Ingredients.Find(ingredientToUser.IngredientId) ??
            throw new NotFoundException($"Ingredient with id {ingredientToUser.IngredientId} not found");
            
        return new FridgeIngredientDTO(
            IngredientId: ingredientToUser.Id,
            Ingredient: ingredient.Name,
            Quantity: ingredientToUser.Quantity,
            Unit: ingredientToUser.Unit,
            ImageUrl: ingredient.ImageUrl
        );
    }

    public async Task<List<FridgeIngredientDTO>> ReadFridge(Guid userId)
    {
        var fridge = await dbContext.IngredientsToUsers
            .Where(i => i.UserId == userId)
            .Include(i => i.Ingredient).ToListAsync();

        return [.. fridge.Select(FridgeIngredientToDTO)];
    }

    public async Task<List<FridgeIngredientDTO>> PutFridge(
        Guid userId, List<FridgePutIngredientDTO> fridge
    )
    {
        dbContext.IngredientsToUsers.RemoveRange(
            dbContext.IngredientsToUsers.Where(i => i.UserId == userId)
        );
        var wrongIngredients = fridge
            .Where(i => !dbContext.Ingredients.Any(ing => ing.Id == i.IngredientId))
            .Select(i => i.IngredientId).ToList();
        if (wrongIngredients.Count > 0)
            throw new BadRequestException(
                $"Ingredients with ids {string.Join(", ", wrongIngredients)} do not exist"
            ); 

        var ingredientsToUsers = fridge.Select(i => new IngredientToUser
        {
            UserId = userId,
            IngredientId = i.IngredientId,
            Quantity = i.Quantity,
            Unit = i.Unit
        }).ToList();

        await dbContext.IngredientsToUsers.AddRangeAsync(ingredientsToUsers);
        await dbContext.SaveChangesAsync();
        return [.. ingredientsToUsers.Select(FridgeIngredientToDTO)];
    }

    public async Task<List<FridgeIngredientDTO>> UpdateFridge(
        Guid userId, List<FridgeIngredientDeltaDTO> fridgeDelta
    )
    {
        var fridge = await dbContext.IngredientsToUsers
            .Where(i => i.UserId == userId)
            .Include(i => i.Ingredient).ToListAsync();

        foreach (var ingredientDelta in fridgeDelta)
        {
            var ingredient = fridge.FirstOrDefault(i => i.IngredientId == ingredientDelta.IngredientId);
            if (ingredient == null)
            {
                var newIngredient = new IngredientToUser
                {
                    UserId = userId,
                    IngredientId = ingredientDelta.IngredientId,
                    Quantity = ingredientDelta.QuantityDelta,
                    Unit = ingredientDelta.Unit
                };
                await dbContext.IngredientsToUsers.AddAsync(newIngredient);
                continue;
            }
            double deltaQuantity = UnitExtensions.Convert(ingredientDelta.Unit, ingredient.Unit) 
                ?? throw new BadRequestException(
                    $"Cannot convert {ingredientDelta.Unit.ToLabel()} to {ingredient.Unit.ToLabel()}"
                );
            deltaQuantity *= ingredientDelta.QuantityDelta;
            ingredient.Quantity += deltaQuantity;
            if (ingredient.Quantity <= 0)
                dbContext.IngredientsToUsers.Remove(ingredient);
        }

        await dbContext.SaveChangesAsync();
        return [.. fridge.Select(FridgeIngredientToDTO)];
    }
}