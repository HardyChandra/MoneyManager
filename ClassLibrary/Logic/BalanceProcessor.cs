using ClassLibrary.Models;
using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Logic
{
    public static class BalanceProcessor
    {
        public static int StoreBalance(int UserID, decimal TotalBalance)
        {
            Balance data = new Balance
            {
                UserID = UserID,
                TotalBalance = TotalBalance
            };

            string sql = @"INSERT INTO Balance (UserID, TotalBalance) 
                            VALUES ((SELECT UserID FROM Users WHERE UserID = @UserID), @TotalBalance)";

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}
