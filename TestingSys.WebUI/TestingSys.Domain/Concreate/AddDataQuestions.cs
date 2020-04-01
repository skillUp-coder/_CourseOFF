using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Concreate
{
    public static class AddDataQuestions
    {
        public static List<Question> _Questions = new List<Question>() 
        {
                new Question(){ Title="256*9:", Answer = "2304", ChoiceOne = "2305", ChoiceTwo = "2301", ChoiceThree = "2306", ChoiceFour = "96",AddedTime = DateTime.Now, CourseId = 1  },
                new Question(){ Title="78*5:", Answer = "390", ChoiceOne = "378", ChoiceTwo = "380", ChoiceThree = "389", ChoiceFour = "391",AddedTime = DateTime.Now, CourseId = 1  },
                new Question(){ Title="45+89", Answer = "134", ChoiceOne = "135", ChoiceTwo = "136", ChoiceThree = "133", ChoiceFour = "132",AddedTime = DateTime.Now, CourseId = 1  },
                new Question(){ Title="98*5", Answer = "490", ChoiceOne = "489", ChoiceTwo = "488", ChoiceThree = "495", ChoiceFour = "494",AddedTime = DateTime.Now, CourseId = 1  },

                new Question(){ Title="Сapital of Japan", Answer = "Tokio", ChoiceOne = "Kasugai", ChoiceTwo = "Handa", ChoiceThree = "İtinomiya", ChoiceFour = "Okadzaki",AddedTime = DateTime.Now, CourseId = 2  },
                new Question(){ Title="Сapital of Serbia", Answer = "Belgrade", ChoiceOne = "Valevo", ChoiceTwo = "Kraljevo ", ChoiceThree = "Krusevac", ChoiceFour = "Leskovac",AddedTime = DateTime.Now, CourseId = 2  },
                new Question(){ Title="Сapital of Switzerland ", Answer = "Berne", ChoiceOne = "Altdorf", ChoiceTwo = "Biel", ChoiceThree = "Lausanne ", ChoiceFour = "Lucerne",AddedTime = DateTime.Now, CourseId = 2  },
        };
    }
}
