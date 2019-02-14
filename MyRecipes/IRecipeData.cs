using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRecipes
{
    public interface IRecipeData
    {
        ArrayList GetRecipeHeadings();
        ArrayList SearchRecipes(string keywords);
        Recipe GetRecipe(int id);
        Recipe AddRecipe(string name, string description, int time, string ingredients, string instruction, string image);
        void AddComment(int recipeID, string comment);
        string[] AutoComplete();
    }
}