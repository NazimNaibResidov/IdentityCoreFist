using IdentityCoreFist.IdentityCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IdentityCoreFist.Model
{
    public class RoleDetails
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser>  Memebers { get; set; }
        public IEnumerable<ApplicationUser> NonMemebers { get; set; }
    }
}
