using System;
using System.Collections.Generic;
using System.Text;

namespace University2.Models
{
    public enum Role { Student, Teacher }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Role UserRole { get; set; }
    
        public ICollection<Course> TaughtCourses { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
