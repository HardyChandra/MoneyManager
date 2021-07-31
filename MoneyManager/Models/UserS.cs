using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Models
{
    public class UserS
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "You need to provide a long enough password (min. 8)")]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="You password and confirm password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}