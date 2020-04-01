using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestingSys.Domain.Concreate;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Infrastructure;
using TestingSys.Domain.Infrastructure.Filters;
using TestingSys.WebUI.Infrastructure.ViewModels;
using TestingSys.WebUI.Models;

namespace TestingSys.WebUI.Controllers
{
    /// <summary>
    /// Implementation of the choice of courses. 
    /// There is also sorting by name,
    /// complexity, and number of questions.
    /// </summary>
    [Log]
    [Authorize(Roles ="user")]
    public class CourseController : Controller
    {
        private static Dictionary<int, int> _questionIdList;
        private CourseRepository repository;
        public CourseController()
        {
            repository = new CourseRepository();
        }

        /// <summary>
        /// Implementing course selection and sorting.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult DisplayCourse(string category, int page = 1, string complexity = null, int count = 0)
        {
            
            int PageSize = 2;
                CourseViewModel model = new CourseViewModel() 
                {
                     Courses = repository._Courses.Where(x=> category == null || x.Category == category).Where(x=>complexity == null||x.Complexity == complexity).Where(x=>count == 0|| x.Counter == count).OrderBy(x=>x.CourseId).Skip((page-1)*PageSize).Take(PageSize),
                     info = new InfoOfPage() 
                     {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = category == null ? repository._Courses.Count() : repository._Courses.Where(x => x.Category == category).Count(),
                     },
                     CurrentCategory = category
                };
                List<Course> _Courses = GetCourses();
                ViewBag.Complaxities = new SelectList(_Courses, "Complexity", "Complexity");
                return View(model);
        }
        /// <summary>
        /// Implementation of passing the test with a limited time.
        /// </summary>
        #region Display Test 
        [HttpGet]
        [AllowAnonymous]
        [ExceptionsDetail]
        public ActionResult TestList(int ? id ) 
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else if (id <= 0)
            {
                throw new Exception("Error in class CourseList. Id less or equel null.");
            }
            var modelPage = repository._Questions.Where(x => x.CourseId == id).ToList();
            var minutsCounter = repository._Courses.Where(x => x.CourseId == id).Select(x => x.ExamTime).FirstOrDefault();
            var _Courses = repository._Courses.Where(x => x.CourseId == id).FirstOrDefault();
            var CourseName = _Courses.CourseName;

            ViewBag.CourseName = CourseName;
            MatchQuestionsToCategoryId(modelPage);
            ViewBag.MinutsCounter = minutsCounter;


            return View(modelPage);
        }

        /// <summary>
        /// Data validation and adding data to the database.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult TestList(FormCollection form, int? id) 
        {
            int trueCounter = 0;
            int falseCounter = 0;
            Exam exam = new Exam { StudentId = Convert.ToInt32(Session["StudentId"]), Date = DateTime.Now };
            List<ExamCategory> examCategories = new List<ExamCategory>();
            foreach (var item in _questionIdList)
            {
                if (form[item.Key.ToString()] == null)
                    continue;
                string result = form[item.Key.ToString()];
                string dbAnswer = GetRightAnswers._GetRightAnsweers(item.Key);

                bool isNewCategory = true;
                foreach (ExamCategory examCategory in examCategories.Where(x => x.CategoryId == item.Value))
                {
                    isNewCategory = false;
                    if (result == dbAnswer)
                    {
                        examCategory.TrueCounter += 1;
                        trueCounter += 1;
                    }
                    else
                    {
                        examCategory.FalseCounter += 1;
                        falseCounter += 1;
                    }
                }

                if (!isNewCategory) continue;

                ExamCategory newExamCategory = new ExamCategory { CategoryId = item.Value, TrueCounter = 0, FalseCounter = 0 };
                examCategories.Add(newExamCategory);
                if (result == dbAnswer)
                {
                    newExamCategory.TrueCounter += 1;
                    trueCounter += 1;
                }
                else
                {
                    newExamCategory.FalseCounter += 1;
                    falseCounter += 1;
                }
            }
            exam.TrueCounter = trueCounter;
            exam.FalseCounter = falseCounter;
            exam.Point = trueCounter * 2;

            using (DataContext context = new DataContext()) 
            {
                context.Exams.Add(exam);
                context.SaveChanges();
            }


            return RedirectToAction($"DisplayResultDatas/{id}", "Result");
        }
        #endregion


        /// <summary>
        /// A client with the admin role can add tests to the course or create a new course.
        /// </summary>
        #region CreateCourse
        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateCourse() 
        {
            List<Course> _Courses = GetCourses();
            ViewBag.Categories = new SelectList(_Courses, "CourseId", "Category");
            ViewBag.Complaxities = new SelectList(_Courses, "Complexity", "Complexity");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(FormCollection form) 
        {
            string Title = form["title"];
            string Answer = form["answer"];
            string CategoryId = form["Categories"];
            string ChoiceOne = form["choiceOne"];
            string ChoiceTwo = form["choiceTwo"];
            string ChoiceThree = form["choiceThree"];
            string ChoiceFour = form["choiceFour"];
            int c_Id = Convert.ToInt32(CategoryId);

            var _Counter = repository._Questions.Where(x => x.CourseId == c_Id).Count();

            Question _Question = new Question() 
            {
                Title = Title,
                Answer = Answer,
                CourseId = c_Id,
                AddedTime = DateTime.Now,
                ChoiceOne = ChoiceOne,
                ChoiceTwo = ChoiceTwo,
                ChoiceThree = ChoiceThree,
                ChoiceFour = ChoiceFour,
            };
            using (DataContext context = new DataContext()) 
            {
                var CourseDatas = context.Courses.Find(c_Id);
                CourseDatas.Counter = _Counter + 1;
                context.Questions.Add(_Question);
                context.Entry(CourseDatas).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("DisplayAdminPage", "Admin");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(FormCollection form) 
        {
            string CategoryName = form["name"];
            string Time = form["time"];
            string Complexity = form["Complaxities"];

            Course _Course = new Course() { Category = CategoryName, Complexity = Complexity,CourseName = CategoryName, ExamTime = Convert.ToInt32(Time) };

            using (DataContext context =  new DataContext()) 
            {
                context.Courses.Add(_Course);
                context.SaveChanges();
            }
            return RedirectToAction("CreateCourse", "Course");
        }
        #endregion

        /// <summary>
        /// A client with the admin role can delete a course.
        /// </summary>
        #region DeleteCourse
        [HttpGet]
        [AllowAnonymous]
        public ActionResult CourseDelete() 
        {
            var CourseDatas = GetCourses();
            return View(CourseDatas);
        }

        [HttpGet]
        [AllowAnonymous]
        [ExceptionsDetail]
        public ActionResult CourseDatasDelete(int ? id) 
        {
            if (id <= 0)
            {
                throw new Exception("Error in class DeleteDataCourse. Id less or equel null.");
            }
            using (DataContext context = new DataContext()) 
            {
                var _DatasCourse = context.Courses.Find(id);
                context.Courses.Remove(_DatasCourse);
                context.SaveChanges();
            }
            return RedirectToAction("DisplayAdminPage", "Admin");
        }
        #endregion

        /// <summary>
        /// A client with the admin role can modify course data.
        /// </summary>
        #region editCourse
        [HttpGet]
        [AllowAnonymous]
        public ActionResult EditCourses() 
        {
            List<Course> CoursesDatas = GetCourses();
            return View(CoursesDatas);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EditDatasCourse(int ? id) 
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            using (DataContext context = new DataContext()) 
            {
                Course _Courses = context.Courses.Find(id);
                if (_Courses != null)
                {
                    List<Course> GetCourse = GetCourses();
                    ViewBag.Complaxities = new SelectList(GetCourse, "Complexity", "Complexity");
                    return View(_Courses);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditDatasCourse(FormCollection form, int ? id) 
        {
            string Category = form["category"];
            string Time = form["time"];

            using (DataContext context = new DataContext()) 
            {
                var CourseDatas = context.Courses.Find(id);
                    CourseDatas.Category = Category;
                    CourseDatas.CourseName = Category;
                    CourseDatas.ExamTime =  Convert.ToInt32(Time);
                context.Entry(CourseDatas).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("DisplayAdminPage", "Admin");
        }
        #endregion


        private void MatchQuestionsToCategoryId(List<Question> questions)
        {
            _questionIdList = new Dictionary<int, int>();


            foreach (var item in questions)
            {
                int categoryId = repository._Questions.FirstOrDefault(x => x.QuestionId == item.QuestionId).CourseId;
                _questionIdList.Add(item.QuestionId, categoryId);
            }

        }
        private List<Course> GetCourses()
        {
            return repository._Courses.ToList();
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