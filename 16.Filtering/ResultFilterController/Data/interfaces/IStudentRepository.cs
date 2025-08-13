namespace ResultFilterController.Data.interfaces;

using ResultFilterController.Data.Model;
public interface IStudentRepository
{
    IEnumerable<Student> GetAllStudents();
    Student GetStudentById(int id);
}

