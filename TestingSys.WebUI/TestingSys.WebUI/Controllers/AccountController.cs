using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestingSys.Domain.Entities;
using TestingSys.Domain.Infrastructure;
using TestingSys.Domain.Infrastructure.Filters;
using TestingSys.WebUI.Infrastructure.ViewModels;


namespace TestingSys.WebUI.Controllers
{
    /// <summary>
    /// Logon Implementation.
    /// </summary>
    [Log]
    [Authorize]
    public class AccountController : Controller
    {
        #region Log In.
        /// <summary>
        /// Entry data is displayed for logging in.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        /// <summary>
        /// Checking data for correctness. 
        /// When the data is entered correctly, it redirects to a specific page.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                User user = null;
                using (DataContext context = new DataContext()) 
                {
                    user = context.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                    if (user != null)
                    {
                        if (user.RoleId == 1)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("DisplayAdminPage", "Admin");
                        }
                        else if (user.RoleId == 2)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("DisplayManagerPage", "Manager");

                        }
                        else if (user.RoleId == 3) 
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("DisplayCourse", "Course");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "A user with such a login does not exist!");
                    }

                }  
            }
            return View(model);
        }
        #endregion

        #region Registration
        /// <summary>
        /// Entry data is displayed for registration.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration() 
        {
            return View();
        }

        /// <summary>
        /// Checking data for correctness. 
        /// When the data is entered correctly, it redirects to a specific page.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registration(RegisterViewModel model) 
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DataContext context = new DataContext()) 
                {
                    user = context.Users.FirstOrDefault(x=>x.Email == model.Email);
                }
                if (user == null)
                {
                    using (DataContext context = new DataContext())
                    {
                        int studentId = context.Users.FirstOrDefault(x => x.UserId == x.RoleId).RoleId;
                        Session["StudentId"] = studentId;
                        context.Users.Add(new User() { SessionId = Convert.ToInt32(Session["StudentId"]), Email = model.Email, Password = model.Password, RoleId = 3, FirstName = model.FirstName, LastName = model.LastName });
                        context.SaveChanges();

                        user = context.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("DisplayCourse", "Course");
                    }
                }
                else 
                {
                    ModelState.AddModelError("", "There is already a user with this login!");
                }
            }
            return View(model);
        }
        #endregion

        #region LogOut
        /// <summary>
        /// Performed sign Out.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");
        }
        #endregion
    }

}