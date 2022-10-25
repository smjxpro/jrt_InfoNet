using System.Globalization;
using InfoNet.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace InfoNet.API.Controllers;

public class FileController : BaseController
{
    private readonly IWebHostEnvironment _environment;

    public FileController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<string>>> Upload(IFormFile file)
    {
        var response = new ApiResponse<string>();
        try
        {
            if (file.Length > 0)
            {
                var path = _environment.WebRootPath + "/uploads";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fileName = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture).Replace(":", "-") +
                               Path.GetExtension(file.FileName);
                var uploads = Path.Combine(_environment.WebRootPath, "uploads/",
                    fileName);

                if (file.Length > 0)
                {
                    await using var fileStream = new FileStream(uploads, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                    response.Data = Path.Combine("/uploads/", fileName);
                    return Ok(response);
                }
            }

            response.Success = false;
            response.Message = "File is empty";
            return BadRequest(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(500, response);
        }
    }

    [HttpGet("{fileName}")]
    public async Task<ActionResult<ApiResponse>> Download(string fileName)
    {
        var response = new ApiResponse();
        try
        {
            var path = _environment.WebRootPath + "/uploads/" + fileName;
            if (!System.IO.File.Exists(path))
            {
                response.Success = false;
                response.Message = "File not found";
                return NotFound(response);
            }

            var memory = new MemoryStream();
            await using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return StatusCode(500, response);
        }

    }

    private static string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    private static Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
        {
            { ".txt", "text/plain" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/vnd.ms-word" },
            { ".docx", "application/vnd.ms-word" },
            { ".xls", "application/vnd.ms-excel" },
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".csv", "text/csv" }
        };
    }
}