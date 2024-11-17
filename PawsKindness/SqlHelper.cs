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

        public static int? ExecuteScalar(string sqlCommand)
        {
            _connection.Open();
            
            SqlCommand cmd = new SqlCommand(sqlCommand, _connection);
            int? result = (int?)cmd.ExecuteScalar();

            _connection.Close();

            return result;
        }

        public static SqlDataReader ExecuteReader(string sqlCommand)
        {
            _connection.Open();

            SqlCommand cmd = new SqlCommand(sqlCommand, _connection);
            var reader = cmd.ExecuteReader();

            _connection.Close();

            return reader;
        }

        public static int ExecuteNotQuery(string insertCmd)
        {
            _connection.Open();

            SqlCommand cmd = new SqlCommand(insertCmd, _connection);
            int result = cmd.ExecuteNonQuery();

            _connection.Close();

            return result;
        }

        public static void CompleteConnect(Action action)
        {
            _connection.Open();
            action();
            _connection.Close();
        }
    }
}