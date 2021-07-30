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

        public static List<Balance> LoadBalance(int UserID)
        {
            Balance data = new Balance()
            {
                UserID = UserID
            };

            string sql = @"SELECT * FROM Balance WHERE UserID = @UserID";

            return SqlDataAccess.LoadData<Balance>(sql, data);
        }

        public static Balance LoadBalanceByID(int BalanceID)
        {
            Balance data = new Balance()
            {
                BalanceID = BalanceID
            };

            string sql = @"SELECT * FROM Balance WHERE BalanceID = @BalanceID";

            return SqlDataAccess.GetData<Balance>(sql, data);
        }

        public static int DeleteBalance(int BalanceID)
        {
            Balance data = new Balance()
            {
                BalanceID = BalanceID
            };

            string sql = @"DELETE FROM Balance WHERE BalanceID = @BalanceID";

            return SqlDataAccess.DeleteData<Balance>(sql, data);
        }

        //public static Balance GetTotalBalance(int UserID)
        //{
        //    Balance data = new Balance
        //    {
        //        UserID = UserID
        //    };

        //    string sql = @"SELECT SUM(TotalBalance) AS 'SumBalance' FROM Balance WHERE UserID = @UserID";

        //    return SqlDataAccess.GetData<Balance>(sql, data);
        //}
    }
}
