using CRUDUsingDapper.Models;
using System.Collections.Generic;

namespace CRUDUsingDapper.IServices
{
    public interface IStudentService
    {
        Student Save(Student student);
        List<Student> Gets();
        Student Get(int studentId);
        string Delete(int studentId);
    }
}
