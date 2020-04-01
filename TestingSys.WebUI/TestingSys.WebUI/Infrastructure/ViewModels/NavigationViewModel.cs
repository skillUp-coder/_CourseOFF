using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingSys.WebUI.Infrastructure.ViewModels
{
    public class NavigationViewModel
    {
        public IEnumerable<string> Complexity { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<int> CounterQuestions { get; set; }
    }
}