﻿using IdentityCoreFist.IdentityCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCoreFist.Infrastructure
{
    [HtmlTargetElement("td",Attributes = "identity-role")]
    public class RoleUserTagHelper:TagHelper
    {
        
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public RoleUserTagHelper(UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }
        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            var role =await roleManager.FindByIdAsync(Role);
            if (role!=null)
            {
                foreach (var user in userManager.Users)
                {
                    if (user!=null&& await userManager.IsInRoleAsync(user,role.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }
            output.Content.SetContent(names.Count == 0 ? "No User" : string.Join(",", names));
        }
    }
}
