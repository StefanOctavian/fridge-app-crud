using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Crud.Entities;

namespace Crud.EntityConfigurations;

public class UserActivationTokenConfiguration : IEntityTypeConfiguration<UserActivationToken>
{
    public void Configure(EntityTypeBuilder<UserActivationToken> builder)
    {
        builder.Property(e => e.Id)
            .IsRequired();
        builder.HasKey(e => e.Id); 

        builder.Property(e => e.UserId)
            .IsRequired();
        
        builder.Property(e => e.Token)
            .HasMaxLength(255) 
            .IsRequired();
        
        builder.Property(e => e.ExpirationDate)
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<UserActivationToken>(e => e.UserId)
            .HasPrincipalKey<User>(e => e.Id);
    }
}