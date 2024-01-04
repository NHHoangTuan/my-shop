using Microsoft.Data.SqlClient;
using MyShop.DTO;
using MyShop.SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    internal class UserDAO:SQLData
    {

        public User GetOne(string username, string password)
        {
            var query = "SELECT * FROM _user WHERE Username = @Username AND Password = @Password";

            var command = new SqlCommand(query, Connection);
            AesHelper aesHelper = new AesHelper();
            string encryptedText = aesHelper.Encrypt(password);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", encryptedText);

            var reader = command.ExecuteReader();

            User user = null;
            if (reader.Read())
            {
                user = new User
                {
                    UserID = (int)reader["UserID"],
                    Username = (string)reader["Username"],
                    Password = (string)reader["Password"],
                   
                };
            }
            reader.Close();
            return user;
        }
        public bool CreateUser(User user)
        {
            var query = "INSERT INTO _user (Username, Password) VALUES (@Username, @Password)";

            var command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
           

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
    }
}
