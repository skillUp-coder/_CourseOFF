using System.Collections.Generic;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Concreate
{
    public static class AddDatasUser
    {
        public static List<User> _Users = new List<User>() 
        {
            new User(){ UserId = 1, Email="admin@gmail.com", Password = "Admin_123", RoleId = 1, },
            new User(){ UserId = 2, Email = "manager@gmail.com", Password = "Manager_123", RoleId = 2,},
            new User(){ UserId = 3, Email="student@gmail.com", Password="Student_123", RoleId = 3, FirstName = "Bruno", LastName = "Mars"},
        };
    }
}
