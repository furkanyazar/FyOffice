using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmployeeEquipmentRepository : EfRepositoryBase<EmployeeEquipment, BaseDbContext>, IEmployeeEquipmentRepository
{
    public EmployeeEquipmentRepository(BaseDbContext context) : base(context)
    {
    }
}