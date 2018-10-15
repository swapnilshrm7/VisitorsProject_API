using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VisitorsProjectAPI.Controllers
{
    public class SQLConnection
    {
        public static SqlConnection SQLConnectionEstablishment()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                connection.ConnectionString = "Data Source=TAVDESK071\\SQLEXPRESS;Initial Catalog=VisitorsDb;User ID=Sa;Password=test123!@#";
                return connection;
            }
            catch (SqlException e)
            {
                return null;
            }
        }
    }
}