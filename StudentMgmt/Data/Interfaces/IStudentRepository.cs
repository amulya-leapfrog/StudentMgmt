using StudentMgmt.Models;

namespace StudentMgmt.Data.Interfaces
{
    public interface IStudentRepository
    {
            Task<IEnumerable<Student>> GetAll();
            Task<Student?> GetById(Guid studentId);
            Task<Student> Add(Student student);
            Task<Student?> Update(Student student);
            Task<Student?> Delete(Student student);
        }
    }

