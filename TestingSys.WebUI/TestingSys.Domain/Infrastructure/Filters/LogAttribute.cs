using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestingSys.Domain.Entities;

namespace TestingSys.Domain.Infrastructure.Filters
{
    public class LogAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            Visitor visitor = new Visitor()
            {
                Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now,

            };
            using (DataContext context = new DataContext())
            {
                context.Visitors.Add(visitor);
                context.SaveChanges();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
