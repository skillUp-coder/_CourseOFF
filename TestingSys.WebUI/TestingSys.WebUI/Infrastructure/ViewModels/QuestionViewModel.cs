using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestingSys.Domain.Entities;

namespace TestingSys.WebUI.Infrastructure.ViewModels
{
    public class QuestionViewModel
    {
        [Required]
        public IEnumerable<Question> _Questions { get; set; }
    }
}