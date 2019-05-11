using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityCoreFist.IdentityCore;
using IdentityCoreFist.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCoreFist.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> _userManager,
         SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var resulte = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (resulte.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password ");
                }
            }
            return View(model);
        }
    }
}