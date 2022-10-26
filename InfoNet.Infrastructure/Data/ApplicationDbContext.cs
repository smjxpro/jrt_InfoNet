using InfoNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoNet.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>().HasData(
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "C#"
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "C++"
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Java"
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "PHP"
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "SQL"
            }
        );
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Skill> Skills { get; set; }
}