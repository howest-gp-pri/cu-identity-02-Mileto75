using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pri.MvcClient.Web.ViewModels
{
    public class AccountRegisterViewModel : AccountLoginViewModel
    {

        [PasswordPropertyText]
        [Compare("Password",ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
