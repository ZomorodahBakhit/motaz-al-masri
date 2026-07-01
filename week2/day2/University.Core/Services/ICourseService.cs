using University.Core.DTOs;
using University.Core.Forms;


namespace University.Core.Services
{
    public interface ICourseService
    {
        CourseDto GetById(int id);
        void Create(CreateCourseForm form);
        void Delete(int id);
    }

    
}