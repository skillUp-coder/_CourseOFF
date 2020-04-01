using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Infrastructure;

namespace TestingSys.Domain.Concreate
{
    public static class GetRightAnswers
    {
        private static DataContext context = new DataContext();
        public static string _GetRightAnsweers(int qId) 
        {
            return context.Questions.FirstOrDefault(x => x.QuestionId == qId).Answer;
        }
    }
}
