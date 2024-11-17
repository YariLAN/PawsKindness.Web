using System;
using System.Data.SqlClient;

namespace PawsKindness.DbModels
{
    public class UserMapper
    {
        public static User ToMap(SqlDataReader userReader)
        {
            if (userReader.Read())
            {
                return new User
                {
                    Id = userReader.GetInt32(0),
                    Surname = userReader.GetString(1),
                    FirstName = userReader.GetString(2),
                    MiddleName = userReader.GetString(3) ?? "",
                    Email = userReader.GetString(4),
                    Phone = userReader.GetString(5),
                    BirthDate = userReader.GetDateTime(6),
                    Address = userReader.GetString(7) ?? "",
                    Role = (RoleEnum)userReader.GetInt32(8),
                    Login = userReader.GetString(9),
                    Password = userReader.GetString(10),
                    IsBlackList = userReader.GetBoolean(11),
                };
            }
            else
            {
                return null;
            }
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; } = string.Empty;
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; } = string.Empty;

        public RoleEnum Role { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public bool IsBlackList { get; set; }   
    }

}