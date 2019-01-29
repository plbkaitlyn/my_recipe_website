using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRecipes
{
    public abstract class IRecipeData
    {
        public abstract Recipe getRecipeData(bool test);
    }
    /*
    public class RecipeFactory : IRecipeData
    {
        public override Recipe getRecipeData(bool test)
        {
            if (test)
            {
                return new RecipeTestData();
            }
            else
                return new RecipeDBData();
        }
    }
    */
}