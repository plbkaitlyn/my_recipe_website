using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
//using System.Windows.Forms;

namespace MyRecipes
{
    public class RecipeDBData
    {
        private static string server = "localhost";
        private static string database = "mysql";
        private static string uid = "root";
        private static string password = "root";

        static string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        private static MySqlConnection connection = new MySqlConnection(connectionString);

        public RecipeDBData() { }  
        
        public ArrayList GetRecipeHeadings()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            string recipeQuery = "SELECT * FROM recipe";

            int recipeID;
            string re_name;
            string description;
            string image;
            
            MySqlCommand recipeCmd = new MySqlCommand(recipeQuery, connection);
            MySqlDataReader recipeDataReader = recipeCmd.ExecuteReader();

            ArrayList _recipes = new ArrayList();

            while (recipeDataReader.Read())
            {
                recipeID = Convert.ToInt32(recipeDataReader["recipeID"]);
                re_name = Convert.ToString(recipeDataReader["re_name"]);
                description = Convert.ToString(recipeDataReader["description"]);
                image = Convert.ToString(recipeDataReader["image"]);

                Recipe recipe = new Recipe(recipeID, re_name, description, image);
                _recipes.Add(recipe);
            }
            connection.Close();
            return _recipes;
            /*
            string json = new JavaScriptSerializer().Serialize(_recipes);
            return json;*/
        }
        
        public ArrayList SearchRecipes(string keyword)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            ArrayList _recipes = new ArrayList();

            string query = "SELECT * FROM recipe";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            int recipeID;
            string re_name;
            string description;
            int re_time;
            string ingredients;
            string instructions;
            string image;
            string username;
            while (dataReader.Read())
            {
                recipeID = Convert.ToInt32(dataReader["recipeID"]);
                re_name = Convert.ToString(dataReader["re_name"]);
                description = Convert.ToString(dataReader["description"]);
                re_time = Convert.ToInt32(dataReader["re_time"]);
                ingredients = Convert.ToString(dataReader["ingredients"]);
                instructions = Convert.ToString(dataReader["instructions"]);
                image = Convert.ToString(dataReader["image"]);
                username = Convert.ToString(dataReader["userName"]);

                Recipe recipe = new Recipe(recipeID, re_name, description, re_time, ingredients, instructions, image, username);
                _recipes.Add(recipe);
            }
            
            ArrayList result = new ArrayList();
            string[] words = keyword.Split(' ');
            for (int i = 0; i < _recipes.Count; i++)
            {
                Recipe recipe = (Recipe)_recipes[i];
                int found = 0;
                for (int j = 0; j < words.Length; j++)
                {
                    if (recipe.Ingredients.IndexOf(words[j]) >= 0)
                    {
                        found = found + 1;
                    }
                }
                if (found == words.Length)
                {
                    result.Add(recipe);
                }
            }
            connection.Close();
            return result;
            /*
            string json = new JavaScriptSerializer().Serialize(result);
            return json;
            */
        }

        public Recipe GetRecipe(int id)
        {
            if(connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            string query = "SELECT * FROM recipe LEFT OUTER JOIN usercomment ON recipe.recipeID = usercomment.recipeID WHERE recipe.recipeID = @id";

            int recipeID;
            string re_name;
            string description;
            int re_time;
            string ingredients;
            string instructions;
            string image;
            string username;
            string cmtUser;
            string comment;
            
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id",id);
            MySqlDataReader recipeDataReader = cmd.ExecuteReader();

            Recipe recipe = null;
            //int count = 0;
            ArrayList _comments = new ArrayList();
            while (recipeDataReader.Read())
            {
                recipeID = Convert.ToInt32(recipeDataReader["recipeID"]);
                re_name = Convert.ToString(recipeDataReader["re_name"]);
                description = Convert.ToString(recipeDataReader["description"]);
                re_time = Convert.ToInt32(recipeDataReader["re_time"]);
                ingredients = Convert.ToString(recipeDataReader["ingredients"]);
                instructions = Convert.ToString(recipeDataReader["instructions"]);
                image = Convert.ToString(recipeDataReader["image"]);
                username = Convert.ToString(recipeDataReader["userName"]);
                //count++;
                recipe = new Recipe(recipeID, re_name, description, re_time, ingredients, instructions, image, username);

                cmtUser = Convert.ToString(recipeDataReader["userCmtName"]);
                comment = Convert.ToString(recipeDataReader["userCmt"]);
                if (comment != "")
                    _comments.Add(new UserComment(cmtUser, comment));
            }
            for (int i = 0; i < _comments.Count; i++)
            {
                UserComment uCmt = (UserComment)_comments[i];
                string user = uCmt.UserName;
                string cmt = uCmt.Comment;
                recipe.AddComment(user, cmt);
            }/*
            while (count > 0)
            {
                cmtUser = Convert.ToString(recipeDataReader["userCmtName"]);
                comment = Convert.ToString(recipeDataReader["userCmt"]);
                if(comment != "")
                    recipe.AddComment(cmtUser, comment);
                count--;
            }
            */
            connection.Close();
            
            if(recipe != null)
            {
                return recipe;
                /*
                string json = new JavaScriptSerializer().Serialize(recipe);
                return json;*/
            }
            return null;
        }
        
        public void AddComment(int recipeId, string comment)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
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
            connection.Close();
        }
    }
}