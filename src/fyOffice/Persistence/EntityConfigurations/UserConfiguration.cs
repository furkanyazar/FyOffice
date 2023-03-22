using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.FirstName).HasColumnName("FirstName");
        builder.Property(p => p.LastName).HasColumnName("LastName");
        builder.Property(p => p.Email).HasColumnName("Email");
        builder.HasIndex(p => p.Email, "UK_Users_Email").IsUnique();
        builder.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
        builder.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
        builder.Property(p => p.Status).HasColumnName("Status").HasDefaultValue(true);
        builder.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
    }
}