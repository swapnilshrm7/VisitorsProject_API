using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VisitorsProjectAPI.Controllers
{
    public class GuardSqlAccess : IRepository
    { 
        public bool UserValidation(string GuardId, string Password)
        {
            SqlConnection connection = SQLConnection.SQLConnectionEstablishment();
            connection.Open();
            string query = "select * from GuardsLog where SecurityId = @GuardId and AccountPassword = @Password";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.Add(new SqlParameter("GuardId", GuardId));
            sqlCommand.Parameters.Add(new SqlParameter("Password", Password));
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
                return true;
            return false;
        } 
    }
}