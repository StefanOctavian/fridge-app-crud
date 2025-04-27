using Microsoft.EntityFrameworkCore;

using Crud.Database;
using Crud.Entities;
using Crud.Entities.Enums;
using Crud.Services.Interfaces;

namespace Crud.Services.Implementations;

public static class SeederService
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FridgeAppDatabaseContext>();

        if (dbContext.Users.FirstOrDefault(u => u.Email == "admin@example.com") == null)
        {
            dbContext.Users.Add(new User 
            {
                Email = "admin@example.com",
                FirstName = "Admin",
                LastName = "Adminescu",
                Password = "L722ZGc07BUwvlmBa4dhth7euvhu8Xci2GMXXO8kk9c=",
                Role = UserRole.Admin,
                IsVerified = true
            });
            dbContext.SaveChanges();
        }
        var adminUser = dbContext.Users.FirstOrDefault(u => u.Email == "admin@example.com")!;

        IEnumerable<Ingredient> ingredients = [
            new() { Name = "Egg", ImageUrl = "https://example.com/egg.jpg" },
            new() { Name = "Milk", ImageUrl = "https://example.com/milk.jpg" },
            new() { Name = "Flour", ImageUrl = "https://example.com/flour.jpg" },
            new() { Name = "Sugar", ImageUrl = "https://example.com/sugar.jpg" },
            new() { Name = "Butter", ImageUrl = "https://example.com/butter.jpg" },
            new() { Name = "Salt", ImageUrl = "https://example.com/salt.jpg" },
            new() { Name = "Pepper", ImageUrl = "https://example.com/pepper.jpg" },
            new() { Name = "Chicken", ImageUrl = "https://example.com/chicken.jpg" },
            new() { Name = "Beef", ImageUrl = "https://example.com/beef.jpg" },
            new() { Name = "Pork", ImageUrl = "https://example.com/pork.jpg" },
            new() { Name = "Fish", ImageUrl = "https://example.com/fish.jpg" },
            new() { Name = "Graham Cracker Powder", ImageUrl = "https://example.com/graham_cracker_powder.jpg" },
            new() { Name = "Vanilla Extract", ImageUrl = "https://example.com/vanilla_extract.jpg" },
            new() { Name = "Baking Powder", ImageUrl = "https://example.com/baking_powder.jpg" },
            new() { Name = "Baking Soda", ImageUrl = "https://example.com/baking_soda.jpg" },
            new() { Name = "Cream Cheese", ImageUrl = "https://example.com/cream_cheese.jpg" },
            new() { Name = "Strawberries", ImageUrl = "https://example.com/strawberries.jpg" },
        ];
        foreach (var ingredient in ingredients)
        {
            if (!dbContext.Ingredients.Any(i => i.Name == ingredient.Name))
            {
                dbContext.Ingredients.Add(ingredient);
            }
            dbContext.SaveChanges();
        }

        if (dbContext.Recipes.FirstOrDefault(r => r.Title == "Simple Cheesecake") == null)
        {
            var recipe = new Recipe
            {
                Title = "Simple Cheesecake",
                Description = "A simple and delicious cheesecake recipe.",
                Body = "1. Preheat the oven to 350°F (175°C).\n2. Mix graham cracker powder, sugar, and melted butter.\n3. Press into the bottom of a springform pan.\n4. Beat cream cheese until smooth.\n5. Add sugar, vanilla extract, and eggs one at a time.\n6. Pour over crust and bake for 50-60 minutes.",
                ImageUrl = "https://example.com/cheesecake.jpg",
                CookingTime = 60,
                Servings = 8,
                Difficulty = RecipeDifficulty.Beginner,
                Ingredients = [
                    new() { IngredientId = dbContext.Ingredients.First(i => i.Name == "Graham Cracker Powder").Id, Quantity = 200, Unit = Unit.Gram },
                    new() { IngredientId = dbContext.Ingredients.First(i => i.Name == "Sugar").Id, Quantity = 100, Unit = Unit.Gram },
                    new() { IngredientId = dbContext.Ingredients.First(i => i.Name == "Butter").Id, Quantity = 100, Unit = Unit.Gram },
                    new() { IngredientId = dbContext.Ingredients.First(i => i.Name == "Cream Cheese").Id, Quantity = 500, Unit = Unit.Gram },
                    new() { IngredientId = dbContext.Ingredients.First(i => i.Name == "Vanilla Extract").Id, Quantity = 10, Unit = Unit.Milliliter },
                    new() { IngredientId = dbContext.Ingredients.First(i => i.Name == "Egg").Id, Quantity = 3, Unit = Unit.Piece },
                    new() { IngredientId = dbContext.Ingredients.First(i => i.Name == "Strawberries").Id, Quantity = 200, Unit = Unit.Gram },
                ]
            };
            dbContext.Recipes.Add(recipe);
            dbContext.SaveChanges();
        }
        var cheesecakeRecipe = dbContext.Recipes.FirstOrDefault(r => r.Title == "Simple Cheesecake")!;

        if (dbContext.Reviews.FirstOrDefault(r => r.UserId == adminUser.Id && r.RecipeId == cheesecakeRecipe.Id) == null)
        {
            var review = new Review
            {
                UserId = adminUser.Id,
                RecipeId = cheesecakeRecipe.Id,
                Rating = 5,
                Comment = "This cheesecake is amazing!",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();
        }
    }
}