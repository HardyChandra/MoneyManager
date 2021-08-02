using DataLibrary.DataAccess;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Logic
{
    public static class UserProcessor
    {
        public static int CreateUser(string Name, string Username, string Password)
        {
            User data = new User
            {
                Name = Name,
                Username = Username,
                Password = Password
            };

            string sql = @"INSERT INTO Users (Name, Username, Password) 
                            VALUES (@Name, @Username, @Password);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static User LoginUser(string Username, string Password)
        {
            User data = new User
            {
                Username = Username,
                Password = Password
            };

            string sql = @"SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

            return SqlDataAccess.GetData<User>(sql, data);
        }

        public static User LoadUserProfile(int UserID)
        {
            User data = new User
            {
                UserID = UserID
            };

            string sql = @"SELECT * FROM Users WHERE UserID = @UserID";

            return SqlDataAccess.GetData<User>(sql, data);
          
        }

        public static int EditUser(int UserID, string Name, string PhoneNumber, string Email)
        {
            User data = new User()
            {
                UserID = UserID,
                Name = Name,
                PhoneNumber = PhoneNumber,
                Email = Email
            };

            string sql = @"UPDATE Users SET PhoneNumber = @PhoneNumber, Email = @Email WHERE UserID = @UserID";

            return SqlDataAccess.UpdateData<User>(sql, data);
        }

        public static int ChangePassword(int UserID,string Password)
        {
            User data = new User()
            {
                UserID = UserID,
                Password = Password
            };

            string sql = @"UPDATE Users SET Password = @Password WHERE UserID = @UserID";

            return SqlDataAccess.UpdateData<User>(sql, data);
        }
    }
}
