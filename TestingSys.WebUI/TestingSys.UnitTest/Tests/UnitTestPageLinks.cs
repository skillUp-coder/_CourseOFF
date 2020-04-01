using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestingSys.WebUI.Infrastructure.Helpers;
using TestingSys.WebUI.Models;

namespace TestingSys.UnitTest.Tests
{
    [TestClass]
    public class UnitTestPageLinks
    {

        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            HtmlHelper myHelper = null;

            InfoOfPage pagingInfo = new  InfoOfPage
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };


            Func<int, string> pageUrlDelegate = i => "Page" + i;
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
    }
}
