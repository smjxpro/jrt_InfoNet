using InfoNet.Domain.Entities;

namespace InfoNet.API.Dtos;

public class PersonCreateDto
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string ResumeLink { get; set; }
    public IEnumerable<Skill> Skills { get; set; }
}