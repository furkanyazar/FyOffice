using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        builder.ToTable("UserOperationClaims").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.UserId).HasColumnName("UserId");
        builder.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
        builder.HasIndex(p => new { p.UserId, p.OperationClaimId },
                         "UK_UserOperationClaims_UserId_OperationClaimId").IsUnique();
        builder.HasOne(p => p.User);
        builder.HasOne(p => p.OperationClaim);
    }
}