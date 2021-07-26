﻿using DataLibrary.Models;
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
        public static int StoreCategory(int UserID, string CategoryName)
        {
            Category data = new Category
            {
                UserID = UserID,
                CategoryName = CategoryName
            };

            string sql = @"INSERT INTO Category (UserID, CategoryName) 
                            VALUES ((SELECT UserID FROM Users WHERE UserID = @UserID), @CategoryName)";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<Category> LoadCategory(int UserID)
        {
            Category data = new Category
            {
                UserID = UserID
            };

            string sql = @"SELECT * FROM Category WHERE UserID = @UserID";

            return SqlDataAccess.LoadData<Category>(sql, data);
        }

        public static Category LoadCategoryByID(int CategoryID)
        {
             Category data = new Category()
            {
                CategoryID = CategoryID
            };

            string sql = @"SELECT * FROM Category WHERE CategoryID = @CategoryID";

            return SqlDataAccess.GetDataByID<Category>(sql, data);
        }

        public static int EditCategory(int CategoryID, string CategoryName)
        {
            Category data = new Category()
            {
                CategoryID = CategoryID,
                CategoryName = CategoryName
            };

            string sql = @"UPDATE Category SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";

            return SqlDataAccess.UpdateData<Category>(sql, data);
        }
    }
}
