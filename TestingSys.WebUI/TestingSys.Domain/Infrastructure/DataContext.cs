using System.Data.Entity;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Infrastructure
{
    public class DataContext:DbContext
    {
        public DataContext() : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }

    }
}
