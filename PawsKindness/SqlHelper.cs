using System;
using System.Configuration;
using System.Data.SqlClient;

namespace PawsKindness
{
    public class SqlHelper
    {
        private static SqlConnection _connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public static SqlDataReader CompleteCommand(string sqlCommand)
        {
            SqlCommand cmd = new SqlCommand(sqlCommand, _connection);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public static void CompleteConnect(Action action)
        {
            _connection.Open();
            action();
            _connection.Close();
        }
    }
}