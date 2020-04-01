using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSys.Domain.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Category { get; set; }
        public int ExamTime { get; set; }
        public string CourseText { get; set; }
        public string Complexity { get; set; }
        public int Counter { get; set; }

    }
}
