namespace Core.Persistence.Repositories;

public class Entity
{
    public int Id { get; set; }
    public DateTime DateOfCreate { get; set; }
    public DateTime? DateOfLastUpdate { get; set; }

    public Entity()
    {
    }

    public Entity(int id) : this()
    {
        Id = id;
    }
}