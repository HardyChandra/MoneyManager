﻿using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Logic
{
    public static class UserProccessor 
    {
        public static int CreateUser(string Name, string Username, string Password)
        {
            User data = new User
            {
                Name = Name,
                Username = Username,
                Password = Password
            };

            string sql = @"INSERT INTO dbo.Users (Name, Username, Password) 
                            VALUES (@Name, @Username, @Password);";

            return SqlDataAccess.SaveData(sql, data);
        }
    }

    public static List<User> LoginUser(string Username, string Password)
    {
        User data = new User
        {
            Username = Username,
            Password = Password
        };

        string sql = @"SELECT * FROM dbo.Users WHERE Username = @Username AND Password = @Password;";

        return SqlDataAccess.LoadData<User>(sql);
    }
}
