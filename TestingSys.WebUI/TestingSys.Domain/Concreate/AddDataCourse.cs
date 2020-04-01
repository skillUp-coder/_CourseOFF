using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Concreate
{
    public static class AddDataCourse
    {
        public static List<Course> _Courses = new List<Course>()
        {
            new Course() {  Counter = 4,  CourseName = "Math", Complexity = "Hard", ExamTime = 10, Category = "Math", CourseText = "Read each question carefully to make sure you understand the type of answer required. If you choose to use a calculator, be sure it is permitted, is working on test day, and has reliable batteries. Use your calculator wisely. Solve the problem. Locate your solution among the answer choices. Make sure you answer the question asked. Make sure your answer is reasonable. Check your work." },
            new Course() {  Counter = 3, CourseName = "Geography", Complexity = "Easy", ExamTime = 5, Category = "Geography", CourseText = "Take the multiple-choice challenge and learn about the continents, countries, oceans and vast bodies of water that are part of planet Earth." }
        };
    }
}
