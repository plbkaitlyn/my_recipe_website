using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace MyRecipes
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RecipeService
    {
        [OperationContract]
        public string GetRecipeHeadings()
        {
            RecipeDBData data = new RecipeDBData();
            ArrayList _recipes = data.GetRecipeHeadings();
            string json = new JavaScriptSerializer().Serialize(_recipes);
            return json;
        }
        
        [OperationContract]
        public string SearchRecipes(string keyword)
        {
            RecipeDBData data = new RecipeDBData();
            ArrayList result = data.SearchRecipes(keyword);

            string json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        
        [OperationContract]
        public string GetRecipe(int id)
        {
            RecipeDBData data = new RecipeDBData();
            Recipe recipe = data.GetRecipe(id);
            string json = new JavaScriptSerializer().Serialize(recipe);
            return json;
            //return data.GetRecipe(id);
        }
        
        [OperationContract]
        public string AddComment(int recipeId, string comment)
        {
            try
            {
                RecipeDBData data = new RecipeDBData();
                data.AddComment(recipeId, comment);
                return "";
            }
            catch (Exception oEx)
            {
                return oEx.Message;
            }
        }

        [OperationContract]
        public string Login(string username, string password)
        {
            try
            {
                UserDBData data = new UserDBData();
                User checkUser = new User(username, password);
                data.Login(checkUser);
                return ""; 
            }
            catch (Exception oEx)
            {
                return oEx.Message;
            }
        }

        [OperationContract]
        public string Register(string username, string email, string password, string repassword)
        {
            try
            {
                UserDBData data = new UserDBData();
                User newUser = new User(username, email, password);
                data.Register(newUser, repassword);
                return "";
            }
            catch (Exception oEx)
            {
                return oEx.Message;
            }
        }

        [OperationContract]
        public string AddRecipe(string name, string description, int time, string ingredients, string instruction, string image)
        {
            try
            {
                UserDBData data = new UserDBData();
                Recipe newRecipe = data.AddRecipe(name, description, time, ingredients, instruction, image);
                string json = new JavaScriptSerializer().Serialize(newRecipe);
                return json;
            }
            catch (Exception oEx)
            {
                return oEx.Message;
            }
        }

        [OperationContract]
        public string Logout()
        {
            UserDBData data = new UserDBData();
            data.Logout();
            return "";
        }
    }
}
