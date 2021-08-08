using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyManager.Models
{
    public class UserProfile
    {
        public int UserID { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Phone Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter the Correct Phone Number!")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Must be a valid Email Address")]
        public string Email { get; set; }
    }
}