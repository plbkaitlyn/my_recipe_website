using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Script.Serialization;

namespace MyRecipes
{
    public class UserTestData
    {
        private static bool _initialised = false;
        private static UserTestData _userData;

        private ArrayList _users = new ArrayList();
        public ArrayList Users
        {
            get { return _users; }
        }

        public static UserTestData GetUserData()
        {
            if (_initialised == false)
            {
                _userData = new UserTestData();
                _initialised = true;
            }
            return _userData;
        }

        public void AddUser(string username, string password)
        {
            User newUser = new User(username, password);
            _users.Add(newUser);
        }

        private UserTestData()
        {
            GenerateData();
        }

        public void Login(string username, string password)
        {
            if (username == "" || password == "")
            {
                throw new System.ArgumentException("Username and/or pasword must have a value!");
            }
            bool found = false;

            for (int i = 0; i < _users.Count; i++)
            {
                User u = (User)_users[i];
                if (username == u.Name)
                    found = true;
            }

            if (found == false)
            {
                throw new System.ApplicationException("Username has not been registered yet!");
            }
            HttpContext.Current.Session["UserName"] = username;
        }

        public void Register(string username, string email, bool errorFlag, string password, string repassword)
        {

            if (username == "" || email == "" || password == "" || repassword == " ")
            {
                throw new System.ArgumentException("No field is empty!");
            }

            if (errorFlag)
            {
                throw new System.ApplicationException("Please provide a valid email address!");
            }

            if (password != repassword)
            {
                throw new System.ApplicationException("Password and confirm password must be the same!");
            }

            bool found = false;
            for (int i = 0; i < _users.Count; i++)
            {
                User u = (User)_users[i];

                if (username == u.Name)
                {
                    found = true;
                    throw new System.ApplicationException("Username has already been registered!");
                }
            }
            if (!found)
            {
                AddUser(username, password);
            }
        }
        
        public Recipe AddRecipe(string name, string description, int time, string ingredients, string instruction, string image)
        {
            if (name == "" || description == "" || time == 0 || ingredients == "" || instruction == "" || image == "")
            {
                throw new System.ArgumentException("No field is empty!");
            }

            string username = HttpContext.Current.Session["UserName"].ToString();
            RecipeTestData data = RecipeTestData.GetRecipeData();
            int length = data.Recipes.Count;

            int id = 0;
            for (int i = 0; i < length; i++)
            {
                Recipe recipe = (Recipe)data.Recipes[i];
                if(i == length - 1)
                    id = recipe.Id + 1;
            }

            Recipe newRecipe = new Recipe(id, name, description, time, ingredients, instruction, image, username);
            data.Recipes.Add(newRecipe);
            return newRecipe;
        }

        public void Logout()
        {
            HttpContext.Current.Session["UserName"] = "";
        }
        
        public void GenerateData()
        {
            string userName = "linh";
            string password = "linh123";
            string email = "linh@gmail.com";
            User user1 = new User(userName, email, password);
            _users.Add(user1);

            userName = "minhanh";
            password = "minhanh1";
            email = "minhanh@hoitmail.com";
            User user2 = new User(userName, email, password);
            _users.Add(user2);

            userName = "evelynj";
            password = "evelynj1";
            email = "evelynj@yahoo.com";
            User user3 = new User(userName, email, password);
            _users.Add(user3);

            userName = "joseh";
            password = "joseh1";
            email = "joseh@gmail.com";
            User user4 = new User(userName, email, password);
            _users.Add(user4);

            userName = "dtx";
            password = "dxt1";
            email = "dxt@gmail.com";
            User user5 = new User(userName, email, password);
            _users.Add(user5);

            userName = "admin";
            password = "admin1";
            email = "admin@gmail.com";
            User user6 = new User(userName, email, password);
            _users.Add(user6);
        }
    }
}
