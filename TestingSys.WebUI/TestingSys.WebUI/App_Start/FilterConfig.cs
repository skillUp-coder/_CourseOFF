using System.Web;
using System.Web.Mvc;
using TestingSys.Domain.Infrastructure.Filters;

namespace TestingSys.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogAttribute());
            filters.Add(new ExceptionsDetailAttribute());
        }
    }
}
