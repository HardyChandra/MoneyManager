using MoneyManager.DataLibrary.DataAccess;
using MoneyManager.DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.DataLibrary.Repository
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

        public static int ChangePassword(int UserID, string Password)
        {
            User data = new User()
            {
                UserID = UserID,
                Password = Password
            };

            string sql = @"UPDATE Users SET Password = @Password WHERE UserID = @UserID";

            return SqlDataAccess.UpdateData<User>(sql, data);
        }

        public static User CheckUsername( )
        {
            User data = new User();

            string sql = @"SELECT Username FROM Users";

            return SqlDataAccess.GetData<User>(sql, data);
        }

        public static User CheckPassword(int UserID)
        {
            User data = new User
            {
                UserID = UserID
            };

            string sql = @"SELECT Password FROM Users WHERE UserID = @UserID";

            return SqlDataAccess.GetData<User>(sql, data);
        }
    }
}

