using System;
using System.Collections.Generic;
using MySqlConnector;

namespace CRUD_DotNet.Models
{
    public class UserModel
    {
        private const string Connection = "Database=crud; Data Source=localhost; User Id=root;";

        public User GetUser(User user)
        {
            var connection = this.CreateConnection();
            connection.Open();

            string query = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password";

            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);

            var reader = command.ExecuteReader();

            var userFinded = new User();

            if (reader.Read())
            {
                userFinded.Id = reader.GetInt32("Id");
                userFinded.Name = reader.GetString("Name");
                userFinded.Email = reader.GetString("Email");
            }

            connection.Close();

            return userFinded;
        } 

        public List<User> GetUsers()
        {
            var connection = this.CreateConnection();
            connection.Open();

            string query = "SELECT * FROM Users;";

            var command = new MySqlCommand(query, connection);

            var reader = command.ExecuteReader();

            List<User> userList = new List<User>();

            while (reader.Read())
            {
                var user = new User();

                user.Id = reader.GetInt32("Id");
                user.Name = reader.GetString("Name");
                user.Email = reader.GetString("Email");

                userList.Add(user);
            }

            connection.Close();

            return userList;
        }

        public void CreateUser(User user)
        {
            var connection = this.CreateConnection();
            connection.Open();

            string query = "INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password);";

            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public void UpdateUser(User user)
        {
            var connection = this.CreateConnection();
            connection.Open();

            string query = "UPDATE SET Users Name=@Name, Email=@Email, Password=@Password WHERE Id=@Id;";

            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public void DeleteUser(int Id)
        {
            var connection = this.CreateConnection();
            connection.Open();

            string query = "DELETE FROM Users WHERE Id=@Id;";

            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", Id);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public User FindUserByID(int Id)
        {
            var connection = this.CreateConnection();
            connection.Open();

            string query = "SELECT * FROM Users WHERE Id=@Id;";

            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", Id);
            
            var reader = command.ExecuteReader();

            var user = new User();

            if (reader.Read())
            {
                user.Id = reader.GetInt32("Id");
                user.Name = reader.GetString("Name");
                user.Email = reader.GetString("Email");
            }

            connection.Close();

            return user;
        }

        private MySqlConnection CreateConnection()
        {
            MySqlConnection myConnection = new MySqlConnection(Connection);
            
            Console.WriteLine("Connection Established With DataBase");

            return myConnection;
        }
    }
}