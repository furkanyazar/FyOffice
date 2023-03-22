using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Equipment : Entity
{
    public string Name { get; set; }
    public short UnitsInStock { get; set; }

    public Equipment()
    {
    }

    public Equipment(int id, string name, short unitsInStock) : this()
    {
        Id = id;
        Name = name;
        UnitsInStock = unitsInStock;
    }
}