using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityCoreFist.IdentityCore;
using IdentityCoreFist.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCoreFist.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPasswordValidator<ApplicationUser> passwordValidator;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;
        public AdminController(UserManager<ApplicationUser> _userManager, IPasswordValidator<ApplicationUser> _passwordValidator,
            IPasswordHasher<ApplicationUser> _passwordHasher
            )
        {
            this.userManager = _userManager;
            this.passwordValidator = _passwordValidator;
            this.passwordHasher = _passwordHasher;
        }
        public IActionResult Index()
        {
            return View(userManager.Users);
        }
        public IActionResult Create()
        {
            RegisterModel model = new RegisterModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel model)
        {
           
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = model.Name;
                user.Email = model.Email;
                var resulte=await  userManager.CreateAsync(user, model.Password);
                if (resulte.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ErrorList(resulte);
                }
            }
         
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var resulte =await userManager.FindByIdAsync(id);
            if (resulte!=null)
            {
                return View(resulte);
            }
            return RedirectToAction("Index", "Home");
          
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateValidtor validtor)
        {
            var user = await userManager.FindByIdAsync(validtor.Id);
            if (user!=null)
            {
                user.Email = validtor.Email;
                IdentityResult myresult = null;
                if (!string.IsNullOrEmpty(validtor.Password))
                {
                    myresult = await passwordValidator.ValidateAsync(userManager, user, validtor.Password);
                    if (myresult.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, validtor.Password);
                    }
                    else
                    {
                        ErrorList(myresult);
                    }
                }
                if (myresult.Succeeded)
                {
                  var resulte=await  userManager.UpdateAsync(user);
                    if (resulte.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        ErrorList(resulte);
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Usern not found");

            }
            return View(user);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var user =await userManager.FindByIdAsync(id);
                var resulte= await userManager.DeleteAsync(user);
                if (resulte.Succeeded)
                {
                    return RedirectToAction("Index",userManager.Users);
                }
                else
                {
                    ErrorList(resulte);
                }
            }
           
            return View(userManager.Users);
        }
        private void ErrorList(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
    }
}