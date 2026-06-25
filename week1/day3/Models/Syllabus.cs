using System;
using System.Collections.Generic;
using System.Text;

namespace University2.Models
{
    public class Syllabus
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Course course { get; set; }
    }
}
