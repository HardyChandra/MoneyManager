using DataLibrary.Models;
using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Logic
{
    public static class CategoryProcessor
    {
        public static int StoreCategory(string Username, string CategoryName)
        {
            Category data = new Category
            {
                Username = Username,
                CategoryName = CategoryName
            };

            string sql = @"INSERT INTO dbo.Category (UserID, CategoryName) 
                            VALUES ((SELECT UserID FROM Users WHERE Username = @Username), @CategoryName)";

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}
