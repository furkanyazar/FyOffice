using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ComputerRepository : EfRepositoryBase<Computer, BaseDbContext>, IComputerRepository
{
    public ComputerRepository(BaseDbContext context) : base(context)
    {
    }
}