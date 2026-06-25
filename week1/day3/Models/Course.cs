using System;
using System.Collections.Generic;
using System.Text;

namespace University2.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? TeacherId { get; set; }
        public User Teacher { get; set; }

        public int? SyllabusId { get; set; }
        public Syllabus Syllabus { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
