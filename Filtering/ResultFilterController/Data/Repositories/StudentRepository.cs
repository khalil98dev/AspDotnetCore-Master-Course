
using ResultFilterController.Data.interfaces;
using ResultFilterController.Data.Model;

namespace ResultFilterController.Data.Repositories;


public class StudentRepository : IStudentRepository
{
    private readonly List<Student> _students = new()
    {
        new Student { Id = 1, Name = "Alice", Age = 20 },
        new Student { Id = 2, Name = "Bob", Age = 22 },
        new Student { Id = 3, Name = "Charlie", Age = 23 }
    };



    public IEnumerable<Student> GetAllStudents()
    {
        return _students;   
    }

    public Student GetStudentById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id) ?? throw new KeyNotFoundException($"Student with ID {id} not found.");
    }
}   
