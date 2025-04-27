using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Crud.Entities;
using Crud.Entities.Enums;

namespace Crud.EntitiesConfigurations;

public class IngredientToRecipeConfiguration : IEntityTypeConfiguration<IngredientToRecipe>
{
    public void Configure(EntityTypeBuilder<IngredientToRecipe> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Quantity)
            .HasDefaultValue(0.0)
            .IsRequired();

        builder.Property(e => e.Unit)
            .HasDefaultValue(Unit.Gram)
            .IsRequired();

        builder.HasOne(e => e.Ingredient)
            .WithMany()
            .HasForeignKey(e => e.IngredientId)
            .IsRequired();

        // Recipe 1-m IngredientToRecipe configured in RecipeConfiguration
    }
}