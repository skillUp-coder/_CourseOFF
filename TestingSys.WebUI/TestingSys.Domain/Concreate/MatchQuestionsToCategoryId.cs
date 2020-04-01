using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Infrastructure;

namespace TestingSys.Domain.Concreate
{
    public static class MatchQuestionsToCategoryId
    {
        private static DataContext context = new DataContext();

        public static void _MatchQuestionsToCategoryId(List<Question> questions, Dictionary<int, int> _questionIdList)
        {
            _questionIdList = new Dictionary<int, int>();

            foreach (var item in questions)
            {
                int categoryId = context.Questions.FirstOrDefault(x => x.QuestionId == item.QuestionId).CourseId;
                _questionIdList.Add(item.QuestionId, categoryId);
            }

        }
    }
}
