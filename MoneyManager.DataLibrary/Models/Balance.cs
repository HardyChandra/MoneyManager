using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.DataLibrary.Models
{
    public class Balance
    {
        public int BalanceID { get; set; }
        public int UserID { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
