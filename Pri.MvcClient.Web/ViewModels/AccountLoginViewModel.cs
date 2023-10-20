using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pri.MvcClient.Web.ViewModels
{
    public class AccountLoginViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        [Required]
        public string Username { get; set; }
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; }
    }
}
