using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
{
    public class RegisterForm
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, MinLength(6)]
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }

}
