using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSys.Domain.Concreate;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Infrastructure;

namespace TestingSys.WebUI.Controllers
{
    /// <summary>
    /// Implementing Administrator Interaction.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private UserRepository repository;
        public AdminController()
        {
            repository = new UserRepository();
        }

        /// <summary>
        /// Admin page display.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult DisplayAdminPage()
        {
            List<User> _Users = repository._Users.Where(x=>x.RoleId == 1).ToList();
            User Users = new User();
            foreach (var itemUser in _Users)
            {
                Users = repository._Users.FirstOrDefault(x => x.UserId == itemUser.UserId);
            }

            return View(Users);
        }

        /// <summary>
        /// All clients with the - user role are displayed.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult DisplayUsers() 
        {
            List<User> _Users = repository._Users.Where(x => x.RoleId >= 3).ToList();
            List<User> Users = new List<User>();
            foreach (var itemUsers in _Users) 
            {
                Users.Add(repository._Users.FirstOrDefault(x=>x.UserId == itemUsers.UserId));
            }
            return View(Users);
        }

        /// <summary>
        /// Implement client lock.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LockOutUsers(int ? id) 
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            using (DataContext context = new DataContext()) 
            {
                User _User = FindUser._GetUser(id);

                if (User!=null) 
                {
                    context.Entry(_User).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    return View(_User);
                }
            }         
            return HttpNotFound(); 
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LockOutUsers(FormCollection form) 
        {
            string FirstName = form["FirstName"];
            string LastName = form["LastName"];
            string Email = form["Email"];
            string Password = form["Password"];
            string RoleId = form["RoleId"];

            List<User> userList = new List<User>()
            {
                new User(){FirstName = FirstName, LastName = LastName, Email = Email, Password = Password, RoleId = 4}
            };

            using (DataContext context = new DataContext()) 
            {
                context.Users.AddRange(userList);
                context.SaveChanges();
            }
            return RedirectToAction("DisplayUsers");
        }
        /// <summary>
        /// Implement client unlock.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult UnLockUsers(int ? id) 
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            User _User = FindUser._GetUser(id);
            if (_User!=null) 
            {
                using (DataContext context = new DataContext()) 
                {
                    context.Entry(_User).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    return View(_User);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UnLockUsers(FormCollection form) 
        {
            string FirstName = form["FirstName"];
            string LastName = form["LastName"];
            string Email = form["Email"];
            string Password = form["Password"];
            List<User> userList = new List<User>()  {new User(){FirstName = FirstName, LastName = LastName, Email = Email, Password = Password, RoleId = 3,SessionId = Convert.ToInt32(Session["StudentId"])}};
            using (DataContext context = new DataContext()) 
            {
                context.Users.AddRange(userList);
                context.SaveChanges();
                
            }
            return RedirectToAction("DisplayUsers");
        }
        /// <summary>
        /// Cleaning unmanaged objects.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}