using System.Collections.Generic;
using University.Core.DTOs;
using University.Core.Forms;
namespace University.Core.Services
{
    public interface IStudentService
    {
        StudentDto GetById(int id);
        List<StudentDto> GetAll();
        void Create(CreateStudentForm form);
        void Update(int id, CreateStudentForm form);
        void Delete(int id);
    }
}
