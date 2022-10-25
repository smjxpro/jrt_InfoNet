using InfoNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoNet.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Skill> Skills { get; set; }
}