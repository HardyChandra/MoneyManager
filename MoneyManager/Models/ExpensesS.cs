using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyManager.Models
{
    public class ExpensesS
    {
        public int ExpensesID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Expenses Category")]
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryID { get; set; }
        [Display(Name = "Expenses Detail")]
        [Required(ErrorMessage = "Expenses Detail is required.")]
        public string ExpensesDetail { get; set; }
        [Display(Name = "Total Expenses")]
        [Required(ErrorMessage = "Total Expenses is required.")]
        public decimal TotalExpenses { get; set; }
    }
}