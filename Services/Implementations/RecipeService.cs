using Microsoft.EntityFrameworkCore;

using Crud.Services.Interfaces;
using Crud.DTOs;
using Crud.Entities;
using Crud.Errors;
using Crud.Database;

namespace Crud.Services.Implementations;

public class RecipeService(FridgeAppDatabaseContext dbContext) : IRecipeService
{
    private async Task<List<IngredientToRecipe>> IngredientsByName(List<IngredientQuantityAddDTO> list)
    {
        List<IngredientToRecipe> ingredients = [];
        foreach (var iq in list)
        {
            var ingredient = await dbContext.Ingredients
                .FirstOrDefaultAsync(i => i.Name == iq.Ingredient)
                ?? throw new NotFoundException($"Ingredient {iq.Ingredient} not found");
            
            ingredients.Add(new IngredientToRecipe
            {
                IngredientId = ingredient.Id,
                Quantity = iq.Quantity,
                Unit = iq.Unit
            });
        }
        return ingredients;
    }

    private static RecipeDTO RecipeToDTO(Recipe recipe)
    {
        return new RecipeDTO
        {
            Id = recipe.Id,
            Name = recipe.Title,
            ImageUrl = recipe.ImageUrl,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Servings = recipe.Servings,
            Difficulty = recipe.Difficulty,
            Ingredients = [.. recipe.Ingredients.Select(i => new IngredientQuantityDTO(
                IngredientId: i.IngredientId,
                Ingredient: i.Ingredient.Name, 
                Quantity: i.Quantity, 
                Unit: i.Unit
            ))],
            Body = recipe.Body,
            Reviews = [.. recipe.Reviews.Select(ReviewService.ReviewToDTO)]
        };
    }

    public async Task Create(RecipeCreateDTO recipe)
    {
        await dbContext.Set<Recipe>().AddAsync(new Recipe
        {
            Title = recipe.Title,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            Servings = recipe.Servings,
            Difficulty = recipe.Difficulty,
            ImageUrl = recipe.ImageUrl,
            VideoUrl = recipe.VideoUrl,
            Body = recipe.Body,
            Ingredients = await IngredientsByName(recipe.Ingredients)
        });

        await dbContext.SaveChangesAsync();
    }

    public async Task<RecipeDTO> Read(Guid id)
    {
        var recipe = await dbContext.Set<Recipe>()
            .Include(r => r.Ingredients)
            .ThenInclude(i => i.Ingredient)
            .Include(r => r.Reviews)
            .FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new NotFoundException($"Recipe with id {id} not found");

        return RecipeToDTO(recipe);
    }

    public async Task<List<RecipeDTO>> Read(bool withIngredients = false, bool withReviews = false)
    {
        IQueryable<Recipe> recipeSet = dbContext.Set<Recipe>();
        if (withIngredients)
            recipeSet = recipeSet.Include(r => r.Ingredients).ThenInclude(i => i.Ingredient);
        if (withReviews)
            recipeSet = recipeSet.Include(r => r.Reviews);

        var recipes = await recipeSet.ToListAsync();
        return [..recipes.Select(RecipeToDTO)];
    }

    public async Task<RecipeDTO> Update(Guid id, RecipeUpdateDTO recipeUpdates)
    {
        var recipe = await dbContext.Set<Recipe>()
            .Include(r => r.Ingredients)
            .ThenInclude(i => i.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new NotFoundException($"Recipe with id {id} not found");

        recipe.Title = recipeUpdates.Title ?? recipe.Title;
        recipe.Description = recipeUpdates.Description ?? recipe.Description;
        recipe.CookingTime = recipeUpdates.CookingTime ?? recipe.CookingTime;
        recipe.Servings = recipeUpdates.Servings ?? recipe.Servings;
        recipe.Difficulty = recipeUpdates.Difficulty ?? recipe.Difficulty;
        recipe.ImageUrl = recipeUpdates.ImageUrl ?? recipe.ImageUrl;
        recipe.VideoUrl = recipeUpdates.VideoUrl ?? recipe.VideoUrl;
        recipe.Body = recipeUpdates.Body ?? recipe.Body;
        if (recipeUpdates.Ingredients != null)
        {
            dbContext.Set<IngredientToRecipe>().RemoveRange(recipe.Ingredients);
            recipe.Ingredients = await IngredientsByName(recipeUpdates.Ingredients);
        }
        recipe.UpdatedAt = DateTime.UtcNow;

        dbContext.Set<Recipe>().Update(recipe);
        await dbContext.SaveChangesAsync();

        return RecipeToDTO(recipe);
    }

    public async Task Delete(Guid id)
    {
        var recipe = await dbContext.Set<Recipe>()
            .Include(r => r.Ingredients)
            .ThenInclude(i => i.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new NotFoundException($"Recipe with id {id} not found");

        dbContext.Set<IngredientToRecipe>().RemoveRange(recipe.Ingredients);
        dbContext.Set<Recipe>().Remove(recipe);
        await dbContext.SaveChangesAsync();
    }
}