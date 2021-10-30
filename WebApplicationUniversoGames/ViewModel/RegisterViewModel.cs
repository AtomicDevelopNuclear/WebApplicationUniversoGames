using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUniversoGames.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and confirmation does not match!")]
        public string ConfirmPassword { get; set; }
    }
}
