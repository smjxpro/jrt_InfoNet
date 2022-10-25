using InfoNet.Domain.Entities;

namespace InfoNet.Domain.Repositories;

public interface IPersonRepository : IGenericRepository<Person, Guid>
{
}