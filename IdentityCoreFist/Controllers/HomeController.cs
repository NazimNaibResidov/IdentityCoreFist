using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityCoreFist.IdentityCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCoreFist.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
    }
}