using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestingSys.WebUI.Controllers;

namespace TestingSys.UnitTest.Tests
{
    [TestClass]
     public class UnitTestError
    {
        [TestMethod]
        public void PageNotFoungNotNull() 
        {
            ErrorController controller = new ErrorController();

            ViewResult result = controller.PageNotFound() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void RangeError()
        {
            ErrorController controller = new ErrorController();

            ViewResult result = controller.RangeError() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
