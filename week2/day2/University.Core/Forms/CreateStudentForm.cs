using System.ComponentModel.DataAnnotations;
namespace University.Core.Forms
{
    public class CreateStudentForm
    {
        [Required(ErrorMessage = "Student Name is required, and MUST be A string!!!")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Student Email is required, and MUST be A string!!!")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = null!;
    }
}
