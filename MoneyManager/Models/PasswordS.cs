using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyManager.Models
{
    public class PasswordS
    {
        public int UserID { get; set; }

        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Current Password is required.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm New Password")]
        [Compare("Password", ErrorMessage = "You new password and confirm new password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}