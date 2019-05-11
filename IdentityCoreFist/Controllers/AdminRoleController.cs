using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityCoreFist.IdentityCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCoreFist.Controllers
{
    public class AdminRoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public AdminRoleController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View(roleManager.Roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (ModelState.IsValid)
            {
                var resulte = await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = name
                });
                if (resulte.Succeeded)
                {
                    return RedirectToAction("Index", roleManager.Roles);
                }
                else
                {
                    foreach (var item in resulte.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(name);
        }
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role =await roleManager.FindByIdAsync(id);
            var memeber =new  List<ApplicationUser>();
            var nonMener= new List<ApplicationUser>();
            foreach (var item in userManager.Users)
            {
                var list =await userManager.IsInRoleAsync(item, role.Name)?memeber:nonMener;
                list.Add(item);
            }
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role!=null)
            {
                var resulte =await roleManager.DeleteAsync(role);
                if (resulte.Succeeded)
                {


                    TempData["message"] = $"{role.Name} has been deleted";
                    return RedirectToAction("Index", roleManager.Roles);
                }
                else
                {
                    foreach (var item in resulte.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(role);
        }


    }
}