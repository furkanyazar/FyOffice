using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ComputerConfiguration : IEntityTypeConfiguration<Computer>
{
    public void Configure(EntityTypeBuilder<Computer> builder)
    {
        builder.ToTable("Computers").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.EmployeeId).HasColumnName("EmployeeId").IsRequired(false);
        builder.Property(p => p.Brand).HasColumnName("Brand");
        builder.Property(p => p.Processor).HasColumnName("Processor").IsRequired(false);
        builder.Property(p => p.Memory).HasColumnName("Memory").IsRequired(false);
        builder.Property(p => p.LicenceKey).HasColumnName("LicenceKey").IsRequired(false);
        builder.Property(p => p.Note).HasColumnName("Note").IsRequired(false);
        builder.HasIndex(p => p.EmployeeId, "UK_Computers_EmployeeId").IsUnique();
        builder.HasOne(p => p.Employee);
    }
}