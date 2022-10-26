using InfoNet.Domain.Entities;
using InfoNet.Domain.Repositories;
using InfoNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfoNet.Infrastructure.Repositories;

public class PersonRepository : GenericRepository<Person, Guid>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override Task<IQueryable<Person>> GetAllAsync()
    {
        return Task.FromResult(Context.Persons.AsNoTracking().Include(p => p.Skills).AsQueryable());
    }

    public override async Task<Person?> GetByIdAsync(Guid id)
    {
        return await Context.Persons.AsNoTracking().Include(p => p.Skills).FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<Person> AddAsync(Person entity)
    {
        entity.Id = Guid.NewGuid();
        Context.Persons.Add(entity);

        return entity;
    }
}