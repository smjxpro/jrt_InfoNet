using InfoNet.API.Dtos;
using InfoNet.Domain;
using InfoNet.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InfoNet.API.Controllers;

public class SkillController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public SkillController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Skill>>>> Get()
    {
        var response = new ApiResponse<IEnumerable<Skill>>();

        try
        {
            var skills = await _unitOfWork.Skills.GetAllAsync();
            response.Data = skills;
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
    public async Task<ActionResult<ApiResponse<Skill>>> Get(Guid id)
    {
        var response = new ApiResponse<Skill>();

        try
        {
            var skill = await _unitOfWork.Skills.GetByIdAsync(id);
            if (skill != null)
            {
                response.Data = skill;
                return Ok(response);
            }

            response.Success = false;
            response.Message = "Skill not found";
            return NotFound(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Skill>>> Post([FromBody] Skill skill)
    {
        var response = new ApiResponse<Skill>();

        try
        {
            await _unitOfWork.Skills.AddAsync(skill);
            await _unitOfWork.CommitAsync();

            response.Data = skill;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<Skill>>> Put(Guid id, [FromBody] Skill skill)
    {
        var response = new ApiResponse<Skill>();

        try
        {
            var skillToUpdate = await _unitOfWork.Skills.GetByIdAsync(id);
            if (skillToUpdate == null)
            {
                response.Success = false;
                response.Message = "Skill not found";
                return NotFound(response);
            }

            if (skillToUpdate.Id != skill.Id)
            {
                response.Success = false;
                response.Message = "Skill id mismatch";
                return BadRequest(response);
            }

            skillToUpdate.Name = skill.Name;

            await _unitOfWork.CommitAsync();

            response.Data = skillToUpdate;
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
    public async Task<ActionResult<ApiResponse<Skill>>> Delete(Guid id)
    {
        var response = new ApiResponse<Skill>();

        try
        {
            var skillToDelete = await _unitOfWork.Skills.GetByIdAsync(id);
            if (skillToDelete == null)
            {
                response.Success = false;
                response.Message = "Skill not found";
                return NotFound(response);
            }

            await _unitOfWork.Skills.DeleteAsync(skillToDelete);
            await _unitOfWork.CommitAsync();

            response.Data = skillToDelete;
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