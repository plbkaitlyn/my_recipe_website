using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;

namespace MyRecipes
{
    public class UserDBData
    {
        private static string server = "localhost";
        private static string database = "mysql";
        private static string uid = "root";
        private static string password = "root";
        static string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        private MySqlConnection connection = new MySqlConnection(connectionString);

        public UserDBData() { }
       
        public void Login(User checkUser)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            
            string query = "SELECT * FROM userdata WHERE userName = @name";    

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@name", checkUser.Name);
            MySqlDataReader dataReader = cmd.ExecuteReader();
           
            bool check = dataReader.HasRows;
            connection.Close();
            if (!check)
            {
                throw new System.ApplicationException("Username has not been registered yet!");
            }
            HttpContext.Current.Session["UserName"] = checkUser.Name;
        }

        public void Register(User newUser, string repassword)
        {
            if (newUser.Password != repassword)
            {
                throw new System.ApplicationException("Password and confirm password must be the same!");
            }

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                string userQuery = "INSERT INTO userdata VALUES (@name, @email, @password)";

                MySqlCommand userCmd = new MySqlCommand(userQuery, connection);
                userCmd.Parameters.AddWithValue("@name", newUser.Name);
                userCmd.Parameters.AddWithValue("@email", newUser.Email);
                userCmd.Parameters.AddWithValue("@password", newUser.Password);
                userCmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (MySqlException ex)
            {
                if(ex.Message.Contains("Duplicate"))
                   throw new System.ApplicationException("Username has already been registered!");
            }
        }

        public Recipe AddRecipe(string name, string description, int time, string ingredients, string instruction, string image)
        {
            string username = HttpContext.Current.Session["UserName"].ToString();
            Recipe tmpRecipe = new Recipe(10, name, description, time, ingredients, instruction, image, username);
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            
            string query = "INSERT INTO recipe(re_name, description, re_time, ingredients, instructions, image, userName) VALUES " +
                "(@name,@description,@time,@ingredients,@instruction,@image, @username)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@ingredients", ingredients);
            cmd.Parameters.AddWithValue("@instruction", instruction);
            cmd.Parameters.AddWithValue("@image", image);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();
            
            string lastIdQuery = "SELECT recipeID FROM recipe WHERE userName = @userName ORDER BY recipeID DESC LIMIT 1";
            MySqlCommand idCmd = new MySqlCommand(lastIdQuery, connection);
            idCmd.Parameters.AddWithValue("@userName", username);
            idCmd.ExecuteNonQuery();
            MySqlDataReader dataReader = idCmd.ExecuteReader();
            bool check = dataReader.HasRows;
            int id = 0;
            if (dataReader.Read())
            {
                id = Convert.ToInt32(dataReader["recipeID"]);
            }
                
            connection.Close();

            Recipe newRecipe = new Recipe(id, name, description, time, ingredients, instruction, image, username);
            return newRecipe;
        }
        
        public void Logout()
        {
            HttpContext.Current.Session["UserName"] = "";
        }
    }
}