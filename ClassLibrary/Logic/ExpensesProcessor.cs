using ClassLibrary.Models;
using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Logic
{
    public static class ExpensesProcessor
    {
        public static int SaveExpenses(int UserID, int CategoryID, string ExpensesDetail, decimal TotalExpenses)
        {
            Expenses data = new Expenses()
            {
                UserID = UserID,
                CategoryID = CategoryID,
                ExpensesDetail = ExpensesDetail,
                TotalExpenses = TotalExpenses,
            };

            string sql = @"INSERT INTO Expenses (UserID, CategoryID, ExpensesDetail, TotalExpenses, CreatedDate)
                            VALUES ((SELECT UserID FROM Users WHERE UserID = @UserID), (SELECT CategoryID FROM Category WHERE CategoryID = @CategoryID),
                            @ExpensesDetail, @TotalExpenses, GETDATE())";

            return SqlDataAccess.SaveData<Expenses>(sql, data);
        }

        public static List<Expenses> LoadExpenses(int UserID)
        {
            Expenses data = new Expenses()
            {
                UserID = UserID
            };

            string sql = @"SELECT Expenses.*, Category.CategoryName FROM Expenses 
                            INNER JOIN Category ON Expenses.CategoryID = Category.CategoryID WHERE Expenses.UserID = @UserID AND Category.UserID = @UserID";

            return SqlDataAccess.LoadData<Expenses>(sql, data);
        }

        public static Expenses LoadExpensesByID(int ExpensesID)
        {
            Expenses data = new Expenses()
            {
                ExpensesID = ExpensesID
            };

            string sql = @"SELECT * FROM Expenses WHERE ExpensesID = @ExpensesID";

            return SqlDataAccess.GetData<Expenses>(sql, data);
        }

        public static int EditExpenses(int ExpensesID, string ExpensesDetail, decimal TotalExpenses)
        {
            Expenses data = new Expenses()
            {
                ExpensesID = ExpensesID,
                ExpensesDetail = ExpensesDetail,
                TotalExpenses = TotalExpenses
            };

            string sql = @"UPDATE Expenses SET ExpensesDetail = @ExpensesDetail, TotalExpenses = @TotalExpenses WHERE ExpensesID = @ExpensesID";

            return SqlDataAccess.UpdateData<Expenses>(sql, data);
        }

        public static int DeleteExpenses(int ExpensesID)
        {
            Expenses data = new Expenses()
            {
                ExpensesID = ExpensesID
            };

            string sql = @"DELETE FROM Expenses WHERE ExpensesID = @ExpensesID";

            return SqlDataAccess.DeleteData<Expenses>(sql, data);
        }
    }
}
