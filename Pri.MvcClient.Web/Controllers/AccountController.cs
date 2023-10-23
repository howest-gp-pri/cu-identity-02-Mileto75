using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pri.Drinks.Core.Entities;
using Pri.MvcClient.Web.ViewModels;

namespace Pri.MvcClient.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel accountRegisterViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(accountRegisterViewModel);
            }
            var user = new ApplicationUser();
            user.UserName = accountRegisterViewModel.Username;
            user.Email = accountRegisterViewModel.Username;
            user.Firstname = accountRegisterViewModel.Firstname;
            user.Lastname = accountRegisterViewModel.Lastname;

            //add the user
            var result = await _userManager.CreateAsync(user,accountRegisterViewModel.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(accountRegisterViewModel);
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel accountLoginViewModel)
        {
            //check the credentials
            var result = await _signInManager.PasswordSignInAsync(accountLoginViewModel.Username,
                accountLoginViewModel.Password,
                false,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Please provide correct credentials...");
                return View(accountLoginViewModel);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
