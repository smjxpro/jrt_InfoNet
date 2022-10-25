using InfoNet.Domain.Repositories;

namespace InfoNet.Domain;

public interface IUnitOfWork
{
    IPersonRepository Persons { get; }
    ISkillRepository Skills { get; }

    Task CommitAsync();
}