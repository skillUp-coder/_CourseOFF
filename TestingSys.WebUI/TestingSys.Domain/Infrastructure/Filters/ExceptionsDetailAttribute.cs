using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Infrastructure.Filters
{
    public class ExceptionsDetailAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionDetail exceptionDetail = new ExceptionDetail()
            {
                ExceotionMessage = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };

            using (DataContext db = new DataContext())
            {
                db.ExceptionDetails.Add(exceptionDetail);
                db.SaveChanges();
            }
            filterContext.Result = new RedirectResult("~/Error/RangeError");
            filterContext.ExceptionHandled = true;
        }
    }
}
