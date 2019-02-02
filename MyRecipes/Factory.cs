using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRecipes
{
    public class Factory
    {
        public static IRecipeData GetRecipeData(bool testMode)
        {
            if (testMode == true)
            {
                RecipeTestData data = RecipeTestData.GetRecipeData();
                return data;
            }
            else
                return new RecipeDBData();
        }

        public static IUserData GetUserData(bool testMode)
        {
            if (testMode == true)
            {
                UserTestData data = UserTestData.GetUserData();
                return data;
            }
            else
                return new UserDBData();
        }
    }
}