using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Concreate;

namespace TestingSys.Domain.Infrastructure
{
    /// <summary>
    /// I create default data in the database.
    /// </summary>
    public class InitializerDataContext:DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            /// <summary>
            /// Role Datas.
            /// </summary>        
            foreach (var itemRoles in AddDatasRole._Roles)
            {
                context.Roles.Add(itemRoles);
            }

            /// <summary>
            /// User Datas.
            /// </summary>        
            foreach (var itemUsers in AddDatasUser._Users) 
            {
                context.Users.Add(itemUsers);
            }

            /// <summary>
            /// Course Datas.
            /// </summary>
            foreach (var itemCourse in AddDataCourse._Courses) 
            {
                context.Courses.Add(itemCourse);
            }

            /// <summary>
            /// Question Datas.
            /// </summary>
            foreach (var itemQuestion in AddDataQuestions._Questions) 
            {
                context.Questions.Add(itemQuestion);
            }

            base.Seed(context);
        }
    }
}
