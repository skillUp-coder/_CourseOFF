using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSys.Domain.Concreate;
using TestingSys.Domain.Entities;
using TestingSys.WebUI.Infrastructure.ViewModels;

namespace TestingSys.WebUI.Controllers
{
    /// <summary>
    /// The Menu controller displays a menu for filtering data.
    /// </summary>
    public class NavigationController : Controller
    {
        NavigationRepository repository;
        public NavigationController()
        {      
            repository = new NavigationRepository();
        }

        [HttpGet]
        public PartialViewResult Menu(string category = null, string complexity = null, int count = 0)
        {
            List<Course> _Course = GetCourses._GetCourses();
            foreach (var itemCourse in _Course) 
            {
                for (int i=0;i<=itemCourse.CourseId;i++) 
                {
                    List<int> _CourseList = new List<int>();
                    var _counter = repository._Questions.Where(x=>x.CourseId == i).Count();
                    ViewBag.Counter = (count == 0) ? _counter : 0;
                }
            }
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedComplexity = complexity;
            NavigationViewModel model = new NavigationViewModel
            {
                Categories = repository._Courses.Select(x => x.Category).Distinct(),
                Complexity = repository._Courses.Select(x => x.Complexity).Distinct(),
                CounterQuestions = repository._Courses.Select(x=>x.Counter).Distinct(),
                
            };
            return PartialView(model);
        }

        



        /// <summary>
        /// Cleaning unmanaged objects.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
        }
    }
}