using InfoNet.Domain;
using InfoNet.Domain.Repositories;
using InfoNet.Infrastructure.Data;
using InfoNet.Infrastructure.Repositories;

namespace InfoNet.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Persons = new PersonRepository(_context);
        Skills = new SkillRepository(_context);
    }

    public IPersonRepository Persons { get; }
    public ISkillRepository Skills { get; }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}