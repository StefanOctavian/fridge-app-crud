using Ardalis.EFCore.Extensions;
using Crud.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud.Database;

public sealed class FridgeAppDatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<IngredientToUser> IngredientsToUsers { get; set; }
    public DbSet<IngredientToRecipe> IngredientsToRecipes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserActivationToken> UserActivationTokens { get; set; }

    public FridgeAppDatabaseContext(
        DbContextOptions<FridgeAppDatabaseContext> options, bool migrate = true
    ) : base(options)
    {
        if (migrate)
        {
            Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("unaccent")
            .ApplyAllConfigurationsFromCurrentAssembly();
    }
}