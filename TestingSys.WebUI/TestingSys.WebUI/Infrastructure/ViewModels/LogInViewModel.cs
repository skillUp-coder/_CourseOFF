using System.ComponentModel.DataAnnotations;

namespace TestingSys.WebUI.Infrastructure.ViewModels
{
    /// <summary>
    /// Checking the correctness of data for entering the system.
    /// </summary>
    public class LogInViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}