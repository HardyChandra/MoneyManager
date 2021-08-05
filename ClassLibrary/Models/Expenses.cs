using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Expenses
    {
        public int ExpensesID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string ExpensesDetail { get; set; }
        public decimal TotalExpenses { get; set; }
        public DateTime CreatedDate { get; set; }

        //Get CategoryName from Category
        public string CategoryName { get; set; }
        //Get Total
        public int Total { get; set; }
    }
}
