using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Infrastructure;

namespace TestingSys.Domain.Concreate
{
    public static class FindUser
    {
        private static DataContext context = new DataContext();
        public static User _GetUser(int ? id ) 
        {
            return context.Users.Find(id);
        }
    }
}
