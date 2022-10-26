using InfoNet.API.Dtos;
using InfoNet.Domain;
using InfoNet.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InfoNet.API.Controllers;

public class PersonController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public PersonController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Person>>>> Get()
    {
        var response = new ApiResponse<IEnumerable<Person>>();

        try
        {
            var persons = await _unitOfWork.Persons.GetAllAsync();
            response.Data = persons;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<Person>>> Get(Guid id)
    {
        var response = new ApiResponse<Person>();

        try
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null)
            {
                response.Success = false;
                response.Message = "Person not found";
                return NotFound(response);
            }

            response.Data = person;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Person>>> Post([FromBody] PersonCreateDto personCreateDto)
    {
        var response = new ApiResponse<Person>();

        var skills = new List<Skill>();

        try
        {
            foreach (var skill in personCreateDto.Skills)
            {
                var skillDb = await _unitOfWork.Skills.GetByIdAsync(skill.Id);
                if (skillDb == null)
                {
                    throw new Exception("Skill not found");
                }

                skills.Add(skillDb);
            }

            var person = new Person()
            {
                Name = personCreateDto.Name,
                City = personCreateDto.City,
                Country = personCreateDto.Country,
                ResumeLink = personCreateDto.ResumeLink,
                DateOfBirth = personCreateDto.DateOfBirth,
                Skills = skills
            };
            await _unitOfWork.Persons.AddAsync(person);


            await _unitOfWork.CommitAsync();

            response.Data = person;
            return CreatedAtAction(nameof(Get), new { id = person.Id }, response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<Person>>> Put(Guid id, [FromBody] Person person)
    {
        var response = new ApiResponse<Person>();
        
        var skills = new List<Skill>();


        try
        {
            var personToUpdate = await _unitOfWork.Persons.GetByIdAsync(id);
            if (personToUpdate == null)
            {
                response.Success = false;
                response.Message = "Person not found";
                return NotFound(response);
            }

            if (personToUpdate.Id != person.Id)
            {
                response.Success = false;
                response.Message = "Person id mismatch";
                return BadRequest(response);
            }
            
            foreach (var skill in person.Skills)
            {
                var skillDb = await _unitOfWork.Skills.GetByIdAsync(skill.Id);
                if (skillDb == null)
                {
                    throw new Exception("Skill not found");
                }

                skills.Add(skillDb);
            }
            
            personToUpdate.Skills = skills;

            await _unitOfWork.Persons.UpdateAsync(personToUpdate);

            await _unitOfWork.CommitAsync();

            response.Data = personToUpdate;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse<Person>>> Delete(Guid id)
    {
        var response = new ApiResponse<Person>();

        try
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null)
            {
                response.Success = false;
                response.Message = "Person not found";
                return NotFound(response);
            }

            await _unitOfWork.Persons.DeleteAsync(person);
            await _unitOfWork.CommitAsync();

            response.Data = person;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}