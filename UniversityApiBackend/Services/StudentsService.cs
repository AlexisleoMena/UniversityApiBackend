using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class StudentsService : IStudentsService
    {
        // TODO: resolve Methods
        IEnumerable<Student> IStudentsService.GetStudentsWithCourses()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Student> IStudentsService.GetStudentsWithNoCourses()
        {
            throw new NotImplementedException();
        }
    }
}
