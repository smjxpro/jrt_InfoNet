using InfoNet.Domain.Entities;
using InfoNet.Domain.Repositories;
using InfoNet.Infrastructure.Data;

namespace InfoNet.Infrastructure.Repositories;

public class PersonRepository: GenericRepository<Person,Guid>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }
}