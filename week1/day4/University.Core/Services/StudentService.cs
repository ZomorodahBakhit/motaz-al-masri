using System.Collections.Generic;
using System.Linq;
using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;
using University.Data.Repositories;
namespace University.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public IEnumerable<StudentDto> GetAll()
        {
            var students = _studentRepository.GetAll();
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            }).ToList();
        }

        public StudentDto GetById(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return null;
            }
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email
            };
        }

        public void Create(CreateStudentForm form) 
        {
            var student = new Student
            {
                Name = form.Name,
                Email = form.Email
            };

            _studentRepository.Add(student);
        }

        public void Update( int id, UpdateStudentForm form)
        {
            var student = _studentRepository.GetById(id);
            if(student != null)
            {
                student.Name = form.Name;
                student.Email = form.Email;
                _studentRepository.Update(student);
            }
        }

        public void Delete(int id)
        {
            _studentRepository.Delete(id);
        }
    }
}
