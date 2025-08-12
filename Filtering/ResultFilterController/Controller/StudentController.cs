using Microsoft.AspNetCore.Mvc;
using ResultFilterController.Data.interfaces;

namespace ResultFilterController.Controller;

[ApiController]
[Route("api/Students")]
public class StudentController(IStudentRepository? studentRepository) : ControllerBase
{

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = studentRepository?.GetAllStudents();

        if (students == null || !students.Any())
        {
            return NotFound("No students found.");
        }

        return Ok(students);

    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        try
        {
            var student = studentRepository?.GetStudentById(id);
            return Ok(student);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    [Route("file/{fileID:int}")]
    public ActionResult GetStudentByFileId(int fileID)
    {
        string filePath = "Curent.pdf";

        var file = Path.Combine(Directory.GetCurrentDirectory(), filePath);

        if (!Path.Exists(file))
            throw new FieldAccessException("File not found"); // Simulate fetching a student by file ID

        return Ok(new
        {
            FileName = Path.GetFileName(file),
            FilePath = file
        });
        
        
    }   
}