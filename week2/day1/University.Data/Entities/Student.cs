using System.ComponentModel.DataAnnotations;

namespace University.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
