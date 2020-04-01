using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Abstract;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Infrastructure;

namespace TestingSys.Domain.Concreate
{
    public class QuestionRepository : IQuestionRepository
    {
        private DataContext context;
        public QuestionRepository()
        {
            context = new DataContext();
        }

        public IEnumerable<Question> _Questions { get { return context.Questions; } set { } }


        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
