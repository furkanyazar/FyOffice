using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Computer : Entity
{
    public int? EmployeeId { get; set; }
    public string Brand { get; set; }
    public string? Processor { get; set; }
    public string? Memory { get; set; }
    public string? LicenceKey { get; set; }
    public string? Note { get; set; }

    public virtual Employee? Employee { get; set; }

    public Computer()
    {
    }

    public Computer(int id, int? employeeId, string brand, string? processor, string? memory, string? licenceKey,
                    string? note) : this()
    {
        Id = id;
        EmployeeId = employeeId;
        Brand = brand;
        Processor = processor;
        Memory = memory;
        LicenceKey = licenceKey;
        Note = note;
    }
}