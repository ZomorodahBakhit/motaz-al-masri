using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository repository, ILogger<StudentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public StudentDto GetById(int id)
        {
            _logger.LogInformation("Attempting to fetch student with ID {Id}", id);

            var student = _repository.GetById(id);
            if (student == null)
            {
                _logger.LogWarning("Student with ID {Id} was not found.", id);
                throw new NotFoundException($"Student with ID {id} does not exist.");
            }

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name
            };
        }

        public List<StudentDto> GetAll()
        {
            _logger.LogInformation("Fetching all students from the database.");

            var students = _repository.GetAll();
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public void Create(CreateStudentForm form)
        {
            _logger.LogInformation("Attempting to create a new student with email {Email}.", form.Email);

            var validationResult = FormValidator.Validate(form);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed while creating a new student.");
                throw new BusinessException(validationResult.Errors);
            }

            var existingStudent = _repository.GetAll().FirstOrDefault(s => s.Email == form.Email);
            if (existingStudent != null)
            {
                _logger.LogWarning("Attempted to create a student with a duplicate email: {Email}", form.Email);
                throw new BusinessException("A student with this email already exists.");
            }

            var student = new Student
            {
                Name = form.Name,
                Email = form.Email
            };

            _repository.Add(student);
            _logger.LogInformation("Successfully created student with Email {Email}", student.Email);
        }

        public void Update(int id, CreateStudentForm form)
        {
            _logger.LogInformation("Attempting to update student with ID {Id}", id);

            var validationResult = FormValidator.Validate(form);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed while updating student ID {Id}.", id);
                throw new BusinessException(validationResult.Errors);
            }

            var student = _repository.GetById(id);
            if (student == null)
            {
                _logger.LogWarning("Cannot update. Student with ID {Id} not found.", id);
                throw new NotFoundException($"Student with ID {id} does not exist.");
            }

            var emailExists = _repository.GetAll().Any(s => s.Email == form.Email && s.Id != id);
            if (emailExists)
            {
                _logger.LogWarning("Cannot update. Email {Email} is already taken by another student.", form.Email);
                throw new BusinessException("This email is already in use by another student.");
            }

            student.Name = form.Name;
            student.Email = form.Email;
            _repository.Update(student);

            _logger.LogInformation("Successfully updated student with ID {Id}", id);
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Attempting to delete student with ID {Id}", id);

            var student = _repository.GetById(id);
            if (student == null)
            {
                _logger.LogWarning("Cannot delete. Student with ID {Id} not found.", id);
                throw new NotFoundException($"Student with ID {id} does not exist.");
            }

            _repository.Delete(id);
            _logger.LogInformation("Successfully deleted student with ID {Id}", id);
        }
    }
}