using Microsoft.EntityFrameworkCore;

using Crud.Services.Interfaces;
using Crud.DTOs;
using Crud.Errors;
using Crud.Entities;
using Crud.Database;

namespace Crud.Services.Implementations;

public class IngredientService(FridgeAppDatabaseContext dbContext) : IIngredientService
{
    public async Task<List<IngredientDTO>> Read()
    {
        return await dbContext.Ingredients
            .Select(i => new IngredientDTO(i.Id, i.Name, i.ImageUrl))
            .ToListAsync();
    }

    public async Task<IngredientDTO> Read(Guid id)
    {
        var ingredient = await dbContext.Ingredients.FindAsync(id)
            ?? throw new NotFoundException($"Ingredient with id {id} not found");

        return new IngredientDTO(ingredient.Id, ingredient.Name, ingredient.ImageUrl);
    }

    public async Task<IngredientDTO> Read(string name)
    {
        var ingredient = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Name == name)
            ?? throw new NotFoundException($"Ingredient with name {name} not found");

        return new IngredientDTO(ingredient.Id, ingredient.Name, ingredient.ImageUrl);
    }

    public async Task<IngredientDTO> Create(IngredientAddDTO ingredient)
    {
        var existingIngredient = await dbContext.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredient.Name);
        if (existingIngredient != null)
            throw new AlreadyExistsException($"Ingredient with name {ingredient.Name} already exists");

        var newIngredient = new Ingredient
        {
            Name = ingredient.Name,
            ImageUrl = ingredient.ImageUrl
        };

        await dbContext.Ingredients.AddAsync(newIngredient);
        await dbContext.SaveChangesAsync();

        return new IngredientDTO(newIngredient.Id, newIngredient.Name, newIngredient.ImageUrl);
    }

    public async Task<IngredientDTO> Update(Guid id, IngredientUpdateDTO ingredient)
    {
        var existingIngredient = await dbContext.Ingredients.FindAsync(id)
            ?? throw new NotFoundException($"Ingredient with id {id} not found");

        existingIngredient.Name = ingredient.Name ?? existingIngredient.Name;
        existingIngredient.ImageUrl = ingredient.ImageUrl ?? existingIngredient.ImageUrl;

        dbContext.Ingredients.Update(existingIngredient);
        await dbContext.SaveChangesAsync();

        return new IngredientDTO(existingIngredient.Id, existingIngredient.Name, existingIngredient.ImageUrl);
    }

    public async Task Delete(Guid id)
    {
        var ingredient = await dbContext.Ingredients.FindAsync(id)
            ?? throw new NotFoundException($"Ingredient with id {id} not found");

        dbContext.Ingredients.Remove(ingredient);
        await dbContext.SaveChangesAsync();
    }
}