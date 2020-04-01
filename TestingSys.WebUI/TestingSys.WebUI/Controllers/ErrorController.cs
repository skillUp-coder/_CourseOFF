using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingSys.WebUI.Controllers
{
    /// <summary>
    /// When a specific error occurs, page data is displayed.
    /// </summary>
    public class ErrorController : Controller
    {

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RangeError()
        {
            return View();
        }
    }
}