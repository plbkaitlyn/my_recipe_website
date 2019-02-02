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
        [DataMember]
        bool testMode = false;

        [OperationContract]
        public string GetRecipeHeadings()
        {
            IRecipeData data = Factory.GetRecipeData(testMode);
            ArrayList _recipes = data.GetRecipeHeadings();
            string json = new JavaScriptSerializer().Serialize(_recipes);
            return json;
        }
        
        [OperationContract]
        public string SearchRecipes(string keyword)
        {
            IRecipeData data = Factory.GetRecipeData(testMode);
            ArrayList result = data.SearchRecipes(keyword);

            string json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        
        [OperationContract]
        public string GetRecipe(int id)
        {
            IRecipeData data = Factory.GetRecipeData(testMode);
            Recipe recipe = data.GetRecipe(id);
            string json = new JavaScriptSerializer().Serialize(recipe);
            return json;
        }

        [OperationContract]
        public string AddRecipe(string name, string description, int time, string ingredients, string instruction, string image)
        {
            try
            {
                IRecipeData data = Factory.GetRecipeData(testMode);
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
        public string AddComment(int recipeId, string comment)
        {
            try
            {
                IRecipeData data = Factory.GetRecipeData(testMode);
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
                IUserData data = Factory.GetUserData(testMode);
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
                IUserData data = Factory.GetUserData(testMode);
                User newUser = new User(username, email, password, repassword);
                data.Register(newUser);
                return "";
            }
            catch (Exception oEx)
            {
                return oEx.Message;
            }
        }

        [OperationContract]
        public string Logout()
        {
            IUserData data = Factory.GetUserData(testMode);
            data.Logout();
            return "";
        }
    }
}
