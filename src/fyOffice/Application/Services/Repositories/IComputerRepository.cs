using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IComputerRepository : IAsyncRepository<Computer>, IRepository<Computer>
{
}