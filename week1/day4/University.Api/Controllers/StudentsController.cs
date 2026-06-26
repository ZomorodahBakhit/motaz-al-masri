using Microsoft.AspNetCore.Mvc;
using University.Core.Forms;
using University.Core.Services;

namespace University.Api.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _studentService.GetAll();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student  = _studentService.GetById(id);
            if(student == null)
            {
                return NotFound("Student Not Found");
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStudentForm form)
        {
            _studentService.Create(form);
            return Ok("Student created successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateStudentForm form)
        {
            _studentService.Update(id, form);
            return Ok("Student updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return Ok("Student deleted successfully");
        }
    }

}
