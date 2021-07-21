﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyManager.Models
{
    public class AddCategory
    {
        public int UserID { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; }
        
    }
}