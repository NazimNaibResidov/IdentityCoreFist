using IdentityCoreFist.IdentityCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCoreFist.Infrastructure
{
    public class CustomerPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (password.Contains(user.UserName))
              { 
                errors.Add(new IdentityError
                {
                    Code="Nese",
                    Description="Kimse"
                });
            
            

            }
           return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}

