using InfoNet.Domain.Entities;
using InfoNet.Domain.Repositories;
using InfoNet.Infrastructure.Data;

namespace InfoNet.Infrastructure.Repositories;

public class SkillRepository:GenericRepository<Skill, Guid>, ISkillRepository
{
    public SkillRepository(ApplicationDbContext context) : base(context)
    {
    }
}