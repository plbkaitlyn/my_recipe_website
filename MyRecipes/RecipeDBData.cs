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
    public class RecipeDBData : IRecipeData
    {
        string _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();

        public RecipeDBData() { }  
        
        public ArrayList GetRecipeHeadings()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string recipeQuery = "SELECT recipeID, re_name, description, image FROM recipe ORDER BY recipeID DESC LIMIT 6";

                MySqlCommand recipeCmd = new MySqlCommand(recipeQuery, connection);
                MySqlDataReader recipeDataReader = recipeCmd.ExecuteReader();

                ArrayList _recipes = new ArrayList();

                while (recipeDataReader.Read())
                {
                    int recipeID = Convert.ToInt32(recipeDataReader["recipeID"]);
                    string re_name = Convert.ToString(recipeDataReader["re_name"]);
                    string description = Convert.ToString(recipeDataReader["description"]);
                    string image = Convert.ToString(recipeDataReader["image"]);

                    Recipe recipe = new Recipe(recipeID, re_name, description, image);
                    _recipes.Add(recipe);
                }
                return _recipes;
            }
        }
        
        public  ArrayList SearchRecipes(string keyword)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                ArrayList _recipes = new ArrayList();

                string query = "SELECT recipeID, re_name, description, image FROM recipe WHERE MATCH(ingredients) AGAINST (@keyword IN NATURAL LANGUAGE MODE)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int recipeID = Convert.ToInt32(dataReader["recipeID"]);
                    string re_name = Convert.ToString(dataReader["re_name"]);
                    string description = Convert.ToString(dataReader["description"]);
                    string image = Convert.ToString(dataReader["image"]);

                    Recipe recipe = new Recipe(recipeID, re_name, description, image);
                    _recipes.Add(recipe);
                }
                return _recipes;
            }
        }

        public Recipe GetRecipe(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM recipe LEFT OUTER JOIN usercomment ON recipe.recipeID = usercomment.recipeID WHERE recipe.recipeID = @id";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader recipeDataReader = cmd.ExecuteReader();

                Recipe recipe = null;

                ArrayList _comments = new ArrayList();
                while (recipeDataReader.Read())
                {
                    if (recipe == null)
                    {
                        int recipeID = Convert.ToInt32(recipeDataReader["recipeID"]);
                        string re_name = Convert.ToString(recipeDataReader["re_name"]);
                        string description = Convert.ToString(recipeDataReader["description"]);
                        int re_time = Convert.ToInt32(recipeDataReader["re_time"]);
                        string ingredients = Convert.ToString(recipeDataReader["ingredients"]);
                        string instructions = Convert.ToString(recipeDataReader["instructions"]);
                        string image = Convert.ToString(recipeDataReader["image"]);
                        string username = Convert.ToString(recipeDataReader["userName"]);
                        recipe = new Recipe(recipeID, re_name, description, re_time, ingredients, instructions, image, username);
                    }
                    string cmtUser = Convert.ToString(recipeDataReader["userCmtName"]);
                    string comment = Convert.ToString(recipeDataReader["userCmt"]);
                    if(comment != "")
                    {
                        recipe.AddComment(cmtUser, comment);
                    }
                }
                return recipe;
            }
        }

        public Recipe AddRecipe(string name, string description, int time, string ingredients, string instruction, string image)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string username = HttpContext.Current.Session["UserName"].ToString();
                Recipe tmpRecipe = new Recipe(8, name, description, time, ingredients, instruction, image, username);

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
                Recipe newRecipe = new Recipe(id, name, description, time, ingredients, instruction, image, username);
                return newRecipe;
            }
        }

        public void AddComment(int recipeId, string comment)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string username = HttpContext.Current.Session["UserName"].ToString();
                if (username == "")
                {
                    throw new System.ApplicationException("Guest user cannot add comment");
                }

                string query = "INSERT INTO usercomment(recipeID, userCmtName, userCmt) VALUES (" +
                    "@id, @username, @comment)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", recipeId);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@comment", comment);
                cmd.ExecuteNonQuery();
            }
        }
    }
}