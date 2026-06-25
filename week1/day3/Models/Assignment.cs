using System;
using System.Collections.Generic;
using System.Text;

namespace University2.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string AssignmentTitle { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Grade> Grades { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
