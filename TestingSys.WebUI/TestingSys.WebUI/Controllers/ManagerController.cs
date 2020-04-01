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
    /// Implementing Manager Interaction
    /// </summary>
    [Authorize(Roles = "manager")]
    public class ManagerController : Controller
    {
        private UserRepository repository;
        public ManagerController()
        {
            repository = new UserRepository();
        }
        /// <summary>
        /// Realization of display of all clients with a role - user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult DisplayManagerPage() 
        {
            var ManagerDatas = repository._Users.Where(x=>x.RoleId>=3).ToList();
            return View(ManagerDatas);
        }
        /// <summary>
        /// When working with the application on the manager’s side, 
        /// it is important to catch possible errors and exceptions that may arise in a timely manner.
        /// </summary>

        #region ExcptionFilter
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoggingPage() 
        {
            var VisitorsDatas = new List<Visitor>();
            using (DataContext context = new DataContext()) 
            {
                VisitorsDatas = context.Visitors.ToList();
            }
            return View(VisitorsDatas);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ExceptionsPage() 
        {
            var ExceptionDatas = new List<ExceptionDetail>();
            using (DataContext context = new DataContext()) 
            {
                ExceptionDatas = context.ExceptionDetails.ToList();
            }
            return View(ExceptionDatas);
        }
        #endregion

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