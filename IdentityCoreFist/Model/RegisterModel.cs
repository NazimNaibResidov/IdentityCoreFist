using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCoreFist.Model
{
    public class RegisterModel
    {
        [Required]
        [UIHint("name")]
        public string Name { get; set; }
        [Required]
        [UIHint("Email")]
        public string Email { get; set; }
        [Required]
        [UIHint("Passsword")]
        public string Password { get; set; }
    }
}
