using IdentityCoreFist.IdentityCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCoreFist.Infrastructure
{
    public class CustomerUserValidator : IUserValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            if (user.Email.ToLower().EndsWith("mail.ru"))
{
              return  Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError()
                {
                    Code="Errors",
                    Description="nothig"
                }));
            }
        }
    }
}
