using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.FirstName).HasColumnName("FirstName");
        builder.Property(p => p.LastName).HasColumnName("LastName");
        builder.Property(p => p.PhoneNumber).HasColumnName("PhoneNumber").IsRequired(false);
        builder.Property(p => p.DateOfBirth).HasColumnName("DateOfBirth").IsRequired(false);
        builder.HasIndex(p => p.PhoneNumber, "UK_Employees_PhoneNumber").IsUnique();
        builder.HasOne(p => p.Computer);
    }
}