using ClassLibrary.Models;
using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Logic
{
    public class ChartProcessor
    {
        public static List<Chart> LoadExpensesChart(int UserID)
        {
            Chart data = new Chart()
            {
                UserID = UserID
            };

            string sql = @"SELECT Category.CategoryName , COUNT(Expenses.CategoryID) AS Total FROM Expenses 
                        INNER JOIN Category ON Expenses.CategoryID = Category.CategoryID WHERE Expenses.UserID = @UserID AND Category.UserID = @UserID 
                        GROUP BY Category.CategoryName";

            return SqlDataAccess.LoadData<Chart>(sql, data);
        }
    }
}
