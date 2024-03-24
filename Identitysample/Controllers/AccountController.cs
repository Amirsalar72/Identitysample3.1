using System.Threading;
using System.Threading.Tasks;
using Identitysample.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identitysample.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true
                };
                var resualt =  await _userManager.CreateAsync(user,model.Password);
                if (resualt.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in resualt.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnurl = null)
        {
            if(_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            ViewData["returnUrl"] = returnurl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl = null)
        {

            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var resualt =  await  _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                if (resualt.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

                if (resualt.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "Account Is Locked";

                    return View(model);
                }
                ModelState.AddModelError("","UserName Or PassWord Is Invalid");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ISEmailAvailable(string email)
        {
            var user = _userManager.FindByEmailAsync(email);
            if (user == null) return Json(true);
            return Json("Email Already Exists");
        }
        public async Task<IActionResult> ISUserNameAvailable(string username)
        {

            var user = _userManager.FindByNameAsync(username);
            if (user == null) return Json(true);
            return Json("Username Already Exists");
        }
    }
}
