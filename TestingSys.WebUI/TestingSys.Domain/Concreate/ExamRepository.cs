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
    public class ExamRepository : IExamRepository
    {     
        private DataContext context;
        public ExamRepository()
        {
            context = new DataContext();
        }
        public IEnumerable<Question> _Questions { get { return context.Questions; } set { } }
        public IEnumerable<Exam> _Exams { get { return context.Exams; } set { } }
        public IEnumerable<Course> _Courses { get { return context.Courses; } set { } }
        public IEnumerable<User> _Users { get { return context.Users; } set { } }


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
