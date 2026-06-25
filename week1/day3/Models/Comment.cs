using System;
using System.Collections.Generic;
using System.Text;

namespace University2.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatCourseDate { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
