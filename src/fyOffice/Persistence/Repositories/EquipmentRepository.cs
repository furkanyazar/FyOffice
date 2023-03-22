using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EquipmentRepository : EfRepositoryBase<Equipment, BaseDbContext>, IEquipmentRepository
{
    public EquipmentRepository(BaseDbContext context) : base(context)
    {
    }
}