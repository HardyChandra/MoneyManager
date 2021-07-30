using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyManager.Models
{
    public class BalanceS
    {   
        public int BalanceID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Balance")]
        [Required(ErrorMessage = "Balance Name is required.")]
        public decimal TotalBalance { get; set; }      
    }
}