using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeEquipmentConfiguration : IEntityTypeConfiguration<EmployeeEquipment>
{
    public void Configure(EntityTypeBuilder<EmployeeEquipment> builder)
    {
        builder.ToTable("EmployeeEquipments").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.EmployeeId).HasColumnName("EmployeeId");
        builder.Property(p => p.EquipmentId).HasColumnName("EquipmentId");
        builder.HasIndex(p => new { p.EmployeeId, p.EquipmentId },
                         "UK_EmployeeEquipments_EmployeeId_EquipmentId").IsUnique();
        builder.HasOne(p => p.Employee);
        builder.HasOne(p => p.Equipment);
    }
}