using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Employee : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public virtual Computer? Computer { get; set; }

    public Employee()
    {
    }

    public Employee(int id, string firstName, string lastName, string? phoneNumber, DateTime? dateOfBirth) : this()
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }
}