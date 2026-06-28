using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Services;
using University.Core.Validations;
using University.Data.Entities;
using University.Data.Repositories;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repository;
    private readonly ILogger<CourseService> _logger;

    public CourseService(ICourseRepository repository, ILogger<CourseService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public CourseDto GetById(int id)
    {
        var course = _repository.GetById(id);
        if (course == null)
        {
            _logger.LogError("Course with ID {Id} not found.", id);
            throw new NotFoundException($"Course with ID {id} does not exist.");
        }
        return new CourseDto { Id = course.Id, Name = course.Name, Weight = course.Weight };
    }

    public void Create(CreateCourseForm form)
    {
        var validation = FormValidator.Validate(form);
        if (!validation.IsValid)
        {
            _logger.LogWarning("Validation failed for creating course.");
            throw new BusinessException(validation.Errors);
        }

        var course = new Course { Name = form.Name, Weight = form.Weight };
        _repository.Add(course);
        _logger.LogInformation("Course {Name} created successfully.", course.Name);
    }

    public void Delete(int id)
    {
        var course = _repository.GetById(id);
        if (course == null)
        {
            _logger.LogError("Failed to delete. Course with ID {Id} not found.", id);
            throw new NotFoundException($"Cannot delete. Course with ID {id} does not exist.");
        }

        _repository.Delete(id);
        _logger.LogInformation("Course with ID {Id} deleted.", id);
    }
}