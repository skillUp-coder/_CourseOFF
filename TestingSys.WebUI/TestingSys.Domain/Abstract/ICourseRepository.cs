using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Abstract
{
    public interface ICourseRepository:IDisposable
    {
        IEnumerable<Course> _Courses { get; set; }
        IEnumerable<Question> _Questions { get; set; }
    }
}
