namespace InfoNet.Domain.Entities;

public class Person : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string ResumeLink { get; set; }
    public IEnumerable<Skill> Skills { get; set; }
}