using Dapper;
using MoneyManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Repository
{
    public class UserRepository
    {
        public SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        //Add new user
        public void AddUser(User objUser)
        {
            //Additing
            try
            {
                connection();
                con.Open();
                con.Execute("AddNewUser", objUser, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}