using System.Collections.Generic;
using TestingSys.Domain.Entities;
using TestingSys.WebUI.Models;

namespace TestingSys.WebUI.Infrastructure.ViewModels
{
    /// <summary>
    /// Designed for presentation in the controller course.
    /// </summary>
    public class CourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public InfoOfPage info { get; set; }
        public string CurrentCategory { get; set; }
    }
}