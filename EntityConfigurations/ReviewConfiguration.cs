using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Crud.Entities;

namespace Crud.EntityConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(e => e.Id); 

        builder.Property(e => e.UserId)
            .IsRequired();
        
        builder.Property(e => e.RecipeId)
            .IsRequired();
        
        builder.Property(e => e.Rating)
            .IsRequired();
        
        builder.Property(e => e.Comment)
            .HasMaxLength(1000) 
            .IsRequired(false);
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .HasPrincipalKey(u => u.Id);
        
        builder.HasOne(rv => rv.Recipe)
            .WithMany(rc => rc.Reviews)
            .HasForeignKey(rv => rv.RecipeId)
            .HasPrincipalKey(rc => rc.Id);
    }
}