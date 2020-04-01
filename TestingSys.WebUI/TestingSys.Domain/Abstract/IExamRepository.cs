using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Abstract
{
    public interface IExamRepository:IDisposable
    {
        IEnumerable<Question> _Questions { get; set; }
        IEnumerable<Course> _Courses { get; set; } 
        IEnumerable<Exam> _Exams { get; set; }
        IEnumerable<User> _Users { get; set; }
    }
}
