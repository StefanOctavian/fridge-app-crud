using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Crud.Entities;
using Crud.Entities.Enums;

namespace Crud.EntitiesConfigurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(4096);

        builder.Property(r => r.ImageUrl)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(r => r.CookingTime)
            .IsRequired();

        builder.Property(r => r.Servings)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(r => r.Difficulty)
            .HasConversion<string>()
            .HasDefaultValue(RecipeDifficulty.Beginner)
            .IsRequired();

        builder.Property(r => r.VideoUrl)
            .HasMaxLength(512)
            .IsRequired(false);

        builder.Property(r => r.Body)
            .IsRequired()
            .HasMaxLength(16384);

        builder.HasMany(r => r.Ingredients)
            .WithOne(itr => itr.Recipe)
            .HasForeignKey(itr => itr.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}