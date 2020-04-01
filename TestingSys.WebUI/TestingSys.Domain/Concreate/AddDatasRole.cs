using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Concreate
{
    public static class AddDatasRole
    {
        public static List<Role> _Roles = new List<Role>() 
        {
            new Role { RoleId = 1, RoleName = "admin" },
            new Role { RoleId = 2, RoleName = "manager" },
            new Role { RoleId = 3, RoleName = "user" },
            new Role { RoleId = 4, RoleName = "ban" }
        };
    }
}
