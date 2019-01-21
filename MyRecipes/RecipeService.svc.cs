using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace MyRecipes
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RecipeService
    {
        [OperationContract]
        public string GetRecipeHeadings()
        {
            RecipeData data = RecipeData.GetRecipeData();
            return data.GetRecipeHeadings();
        }

        [OperationContract]
        public string SearchRecipes(string keyword)
        {
            RecipeData data = RecipeData.GetRecipeData();
            return data.SearchRecipes(keyword);
        }

        [OperationContract]
        public string GetRecipe(int id)
        {
            RecipeData data = RecipeData.GetRecipeData();
            return data.GetRecipe(id);
        }

        [OperationContract]
        public string AddComment(int recipeId, string comment)
        {
            try
            {
                RecipeData data = RecipeData.GetRecipeData();
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
                UserData data = new UserData();
                data.Login(username, password);
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
            UserData data = new UserData();
            data.Logout();
            return "";
        }
    }
}
