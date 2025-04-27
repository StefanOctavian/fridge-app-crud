using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Crud.Entities;
using Crud.Entities.Enums;

namespace Crud.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(e => e.Id); 

        builder.Property(e => e.FirstName)
            .HasMaxLength(255) 
            .IsRequired();
        
        builder.Property(e => e.LastName)
            .HasMaxLength(255) 
            .IsRequired();

        builder.Property(e => e.Email)
            .HasMaxLength(255) 
            .IsRequired();
        builder.HasAlternateKey(e => e.Email);
        
        builder.Property(e => e.Password)
            .HasMaxLength(255) 
            .IsRequired();
        
        builder.Property(e => e.Role)
            .HasConversion(new EnumToStringConverter<UserRole>())
            .HasMaxLength(255) 
            .IsRequired();

        builder.HasMany(e => e.Fridge)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}
