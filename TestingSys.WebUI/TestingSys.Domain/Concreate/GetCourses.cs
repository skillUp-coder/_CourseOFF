using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Infrastructure;

namespace TestingSys.Domain.Concreate
{
    public static class GetCourses
    {
        private static DataContext context = new DataContext();

        public static List<Course> _GetCourses() 
        {
            return context.Courses.ToList();
        }
    }
}
