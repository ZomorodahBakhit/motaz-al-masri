using System.Collections.Generic;
using University.Core.DTOs;
using University.Core.Forms;

namespace University.Core.Services
{
    public interface IStudentService
    {
        IEnumerable<StudentDto> GetAll();
        StudentDto GetById(int id);
        void Create(CreateStudentForm form);
        void Update(int id,UpdateStudentForm form);
        void Delete(int id);
    }
}
