using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSys.Domain.Concreate;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Infrastructure.Filters;
using TestingSys.WebUI.Infrastructure.ViewModels;

namespace TestingSys.WebUI.Controllers
{
    /// <summary>
    ///In the Result controller 
    ///displays the data of the test passed, as well as the graph of courses taken and correct answers.
    /// </summary>
    public class ResultController : Controller
    {
        private ExamRepository repository;
        public ResultController()
        {
            repository = new ExamRepository();
        }

        /// <summary>
        /// Implementation of the display of data from passed customer tests.
        /// </summary>
        [HttpGet]
        [ExceptionsDetail]
        public ActionResult DisplayResultDatas(int ? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else if (id <= 0)
            {
                throw new Exception("Error in class StatisticList. Id less or equel null.");
            }

            List<DateTime> examTimes = GetExamTimes();
            List<SelectListItem> examTimesSelectList = new List<SelectListItem>();
            
            foreach (var item in examTimes)
            {
                examTimesSelectList.Add(new SelectListItem() { Text = item.ToString(), Value = item.ToString() });

            }

            List<Question> questions = repository._Questions.ToList();
            List<Course> courses = new List<Course>();
            List<Exam> exams = GetLastThreeExamByStudentId();

            foreach (Exam item in exams)
            {
                var _truecounter = item.TrueCounter;
                ViewBag._TrueCounter = _truecounter;

                List<Course> _course = repository._Courses.ToList();
                foreach (var itm in _course)
                {
                    var _counter = repository._Questions.Where(x => x.CourseId == id).Count();
                    ViewBag._Count = _counter;
                    var _res = 100 / _counter * _truecounter;
                    ViewBag._Res = _res;
                }
            }
            ViewBag.ExamTimes = examTimesSelectList;
            return View();
        }
        /// <summary>
        /// The implementation of the display of the display of personal data.
        /// </summary>
        [HttpGet]
        public ActionResult PersonalArea() 
        {
            int studentId = GetStudentId();
            List<User> _Users = repository._Users.Where(x => x.SessionId == studentId).ToList();
            User Users = new User();
            foreach (var itemUser in _Users) 
            {
                Users = repository._Users.FirstOrDefault(x=>x.UserId == itemUser.UserId);
            }
            return View(Users);
        }

        /// <summary>
        /// Display of the correct answers of the client in the schedule.
        /// </summary>
        public ActionResult LastFreeExam()
        {
            List<StatisticViewModel> result = new List<StatisticViewModel>();
            List<Exam> exams = GetLastThreeExamByStudentId();
            foreach (Exam item in exams)
            {
                result.Add(new StatisticViewModel() { Point = item.TrueCounter, Date = item.Date.ToString().Split(' ')[0].ToString() });
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<Exam> GetLastThreeExamByStudentId()
        {
            int studentId = GetStudentId();
            return repository._Exams.Where(x => x.StudentId == studentId).OrderByDescending(x => x.Date).Take(3).OrderBy(x => x.Date).ToList();
        }
        private int GetStudentId()
        {
            return Convert.ToInt32(Session["StudentId"]);
        }
        private List<DateTime> GetExamTimes()
        {
            int studentId = GetStudentId();
            List<Exam> exams = repository._Exams.Where(x => x.StudentId == studentId).ToList();
            List<DateTime> examTimes = new List<DateTime>();
            foreach (var item in exams)
            {
                examTimes.Add(item.Date);
            }
            return examTimes;
        }

        /// <summary>
        /// Cleaning unmanaged objects.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }

    }
}