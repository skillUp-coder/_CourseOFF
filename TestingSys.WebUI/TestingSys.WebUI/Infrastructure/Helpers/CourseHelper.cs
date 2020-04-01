using System;
using System.Web.Mvc;
using TestingSys.WebUI.Models;
using System.Text;

namespace TestingSys.WebUI.Infrastructure.Helpers
{
    /// <summary>
    /// Here implemented helper for pagination.
    /// </summary>
    public static class CourseHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, InfoOfPage pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}