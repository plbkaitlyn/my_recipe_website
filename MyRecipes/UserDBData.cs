using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MyRecipes
{
    public class UserDBData : IUserData
    {
        string _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();

        public UserDBData() { }
       
        public void Login(User checkUser)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM userdata WHERE userName = @name";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", checkUser.Name);
                cmd.Parameters.AddWithValue("@password", checkUser.Password);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                bool check = dataReader.HasRows;
                if (!check)
                {
                    throw new System.ApplicationException("Username has not been registered yet!");
                }
                connection.Close();

                connection.Open();
                string query1 = "SELECT * FROM userdata WHERE userName = @name AND userPassword = @password";

                MySqlCommand cmd1 = new MySqlCommand(query1, connection);
                cmd1.Parameters.AddWithValue("@name", checkUser.Name);
                cmd1.Parameters.AddWithValue("@password", checkUser.Password);
                MySqlDataReader dataReader1 = cmd1.ExecuteReader();

                bool check1 = dataReader1.HasRows;
                if (!check1)
                {
                    throw new System.ApplicationException("Please enter the correct password!");
                }
                HttpContext.Current.Session["UserName"] = checkUser.Name;
            }
        }

        public void Register(User newUser)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string userQuery = "INSERT INTO userdata VALUES (@name, @email, @password)";

                    MySqlCommand userCmd = new MySqlCommand(userQuery, connection);
                    userCmd.Parameters.AddWithValue("@name", newUser.Name);
                    userCmd.Parameters.AddWithValue("@email", newUser.Email);
                    userCmd.Parameters.AddWithValue("@password", newUser.Password);
                    userCmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    if (ex.Message.Contains("PRIMARY"))
                    {
                        throw new System.ApplicationException("Username has already been used. Please try another one!");
                    }
                    else if (ex.Message.Contains("user_ck"))
                    {
                        throw new System.ApplicationException("You have already used this email address to register!");
                    }
                }
            }
        }
        
        public void Logout()
        {
            HttpContext.Current.Session["UserName"] = "";
        }
    }
}