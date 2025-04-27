using Crud.DTOs;
using Crud.Entities;

namespace Crud.Services.Interfaces;

public interface IRecipeService
{
    public Task Create(RecipeCreateDTO recipe);
    public Task<List<RecipeDTO>> Read(bool withIngredients = false, bool withReviews = false);
    public Task<RecipeDTO> Read(Guid id);
    public Task<RecipeDTO> Update(Guid id, RecipeUpdateDTO recipeUpdates);
    public Task Delete(Guid id);
}