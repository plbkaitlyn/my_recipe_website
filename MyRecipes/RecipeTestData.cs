﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Script.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace MyRecipes
{
    public class RecipeTestData : IRecipeData
    {
        private static bool _initialised = false; 
        private static RecipeTestData _recipeData;

        private ArrayList _recipes = new ArrayList();
        public ArrayList Recipes
        {
            get { return _recipes; }
        }
        
        public static RecipeTestData GetRecipeData()
        {
            if (_initialised == false)
            {
                _recipeData = new RecipeTestData();
                _initialised = true;
            }
            return _recipeData;
        }
        

        private RecipeTestData()
        {
            GenerateData();
        }
        
        public ArrayList GetRecipeHeadings()
        {
            return _recipes;
        }
        
        public Recipe GetRecipe(int id)
        {
            for (int i = 0; i < _recipes.Count; i++)
            {
                Recipe recipe = (Recipe)_recipes[i];
                if (recipe.Id == id)
                {
                    return recipe;
                }
            }
            return null;
        }

        public ArrayList SearchRecipes(string keyword)
        {
            ArrayList result = new ArrayList();
            string[] words = (keyword.ToLower()).Split(' ');
            for (int i = 0; i < _recipes.Count; i++)
            { 
                Recipe recipe = (Recipe) _recipes[i];
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
            return result;
        }

        public Recipe AddRecipe(string name, string description, int time, string ingredients, string instructions, string image)
        {
            string username = HttpContext.Current.Session["UserName"].ToString();
            
            string keywords = "";
            try
            {
                string path = HttpContext.Current.Server.MapPath("stopwords.txt");
                using (StreamReader sr = File.OpenText(path))
                {
                    string line;
                    ArrayList stopwords = new ArrayList();
                    string[] splitIngredients = (ingredients.ToLower()).Split(' ');
                    while ((line = sr.ReadLine()) != null)
                    {
                        stopwords.Add(line);
                    }
                    sr.Close();

                    int count = 0;
                    for (int i = 0; i < splitIngredients.Length; i++)
                    {
                        string replacement = Regex.Replace(splitIngredients[i], @"[^a-zA-Z]+", String.Empty);
                        for (int j = 0; j < stopwords.Count; j++)
                        {
                            if (!replacement.Equals(stopwords[j]))
                            {
                                count++;
                            }
                        }
                        if (count == stopwords.Count)
                        {
                            if (keywords.Length > 0)
                            {
                                keywords += " ";
                            }
                            keywords += replacement;
                        }
                        count = 0;
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Cannot read the file:");
                Console.WriteLine(e.Message);
            }
            Recipe tmpRecipe = new Recipe(8, name, description, time, ingredients, instructions, image, username, keywords);
            int length = _recipes.Count;
            //RecipeTestData data = RecipeTestData.GetRecipeData();
            //int length = data.Recipes.Count;

            int id = 0;
            for (int i = 0; i < length; i++)
            {
                //Recipe recipe = (Recipe)data.Recipes[i];
                Recipe recipe = (Recipe)_recipes[i];
                if (i == length - 1)
                {
                    id = recipe.Id + 1;
                }
            }
            Recipe newRecipe = new Recipe(id, name, description, time, ingredients, instructions, image, username, keywords);
            //data.Recipes.Add(newRecipe);
            _recipes.Add(newRecipe);
            return newRecipe;
        }

        public void AddComment(int recipeId, string comment)
        {
            string username = HttpContext.Current.Session["UserName"].ToString();
            if (username == "")
            {
                throw new System.ApplicationException("Guest user cannot add comment");
            }
            for (int i = 0; i < _recipes.Count; i++)
            {
                Recipe recipe = (Recipe)_recipes[i];
                if (recipe.Id == recipeId)
                {
                    recipe.AddComment(username, comment);
                }
            }
        }

        public string[] AutoComplete()
        {
            HashSet<string> words = new HashSet<string>();
            for(int i = 0; i < _recipes.Count; i++)
            {
                Recipe recipe = (Recipe)_recipes[i];
                string keywords = recipe.Keywords.ToLower();
                string[] splitwords = keywords.Split(' ');

                words.UnionWith(new HashSet<string>(splitwords));
            }
            return words.ToArray();
        }

        public void GenerateData()
        {
            int id = 100;
            string name = "Chicken Pot Pie";
            string description = "This thick, hearty chicken and veggie slow cooker pot pie eats like a meal, filled with onion, carrots, celery, and peas";
            int time = 60;
            string ingredients = "2 (10.75 ounce) cans condensed cream of chicken soup" + System.Environment.NewLine + "1 1/2 cups chopped carrots" + System.Environment.NewLine + "1 1/2 cups chopped celery" + System.Environment.NewLine + "1 yellow onion, chopped";
            string instructions = "Combine cream of chicken soup, carrots, celery, onion, stock, parsley, paprika, oregano, salt, and black pepper in a slow cooker and stir to combine" + System.Environment.NewLine + "Cook on Low for 7 1/2 hours" + System.Environment.NewLine + "Continue cooking on Low until heated through";
            string image = "images\\6119674.jpg";
            string userName = "admin";
            string keywords = "chicken soup carrots celery onions";
            Recipe recipe1 = new Recipe(id, name, description, time, ingredients, instructions, image, userName, keywords);
            recipe1.AddComment("joseh", "Good!");
            recipe1.AddComment("evelynj", "It takes too much time.");
            _recipes.Add(recipe1);

            id = 101;
            name = "Polish Borscht";
            description = "This delicious vegetarian borscht is made with beets and dried mushrooms and is a traditional dish in Poland on Christmas Eve";
            time = 30;
            ingredients = "6 dried wild mushrooms" + System.Environment.NewLine + "8 medium beets, trimmed" + System.Environment.NewLine + "4 quarts water, or more as needed" + System.Environment.NewLine + "2 cloves garlic, halved";
            instructions = "Place dried mushrooms in a bowl, cover with cold water, and soak for 30 minutes" + System.Environment.NewLine + "While mushrooms are soaking, place beets in a pot, cover with water, and bring to a boil. Reduce heat and simmer until tender, about 30 minutes" + System.Environment.NewLine + "Place sliced beets in a large pot and cover with 4 quarts water. Add drained mushrooms, onions, garlic, allspice, bay leaves, salt, and pepper";
            image = "images\\5136042.jpg";
            keywords = "mushrooms beets garlic";
            Recipe recipe2 = new Recipe(id, name, description, time, ingredients, instructions, image, userName, keywords);
            recipe2.AddComment("evelynj", "I love it!");
            _recipes.Add(recipe2);

            id = 102;
            name = "Butternut Bisque";
            description = "For a winter special occasion or just a weekday meal, this butternut bisque makes a perfect, warming starter course";
            time = 45;
            ingredients = "3 tablespoons butter" + System.Environment.NewLine + "1 large onion, diced" + System.Environment.NewLine + "1 teaspoon kosher salt, plus more to taste, divided" + System.Environment.NewLine + "1 (2 pound) butternut squash";
            instructions = "Melt butter in a pot over medium-low heat. Add onions and a large pinch of salt" + System.Environment.NewLine + "Cut off ends of squash. Carefully cut squash in half lengthwise and remove the seeds. Peel the squash with a vegetable peeler" + System.Environment.NewLine + "Raise heat under pot to medium-high. Stir in tomato paste; cook and stir until mixture begins to caramelize and turn brown, about 2 minutes";
            image = "images\\4684123.jpg";
            keywords = "butter onions butternut bisque";
            Recipe recipe3 = new Recipe(id, name, description, time, ingredients, instructions, image, userName, keywords);
            recipe3.AddComment("joseh", "Thanks!");
            recipe3.AddComment("evelynj", "The instruction is not really clear to me :-(");
            _recipes.Add(recipe3);

            id = 103;
            name = "Homemade Chicken Parmigiana";
            description = "I received this recipe several years ago from a family member who swore it was a great romantic meal. She was right. My husband asks me to make this meal every time we have a date night. The quantity of ingredients look intimidating, but don't let them fool you. It's easy and delicious!";
            time = 40;
            ingredients = "1 tablespoon butter" + System.Environment.NewLine + "1 egg, beaten" + System.Environment.NewLine + "3 (5 ounce) skinless, boneless chicken breast halves" + System.Environment.NewLine + "3/4 cup shredded Mozzarella cheese";
            instructions = "Melt butter in a saucepan over medium heat. Stir in garlic and onion, and cook until the onion has softened and turned translucent, about 2 minutes. Pour in diced tomatoes and sugar." + System.Environment.NewLine + "Stir together bread crumbs, 2 tablespoons Parmesan cheese, and dried oregano; set aside. In a small bowl, whisk together egg and 2 tablespoons milk until blended" + System.Environment.NewLine + "Heat olive oil in a large skillet over medium heat. Add chicken breasts and cook on both sides until they reach an internal temperature of 160 degrees F (70 degrees C)";
            image = "images\\4473147.jpg";
            keywords = "butter chicken breast Mozzarella cheese";
            Recipe recipe4 = new Recipe(id, name, description, time, ingredients, instructions, image, userName, keywords);
            recipe4.AddComment("joseh", "Do not really like the taste");
            _recipes.Add(recipe4);

            id = 104;
            name = "Tuscan Pork Tenderloin";
            description = "This is a very easy weeknight pork tenderloin recipe that is also keto-friendly. Serve with rice for a complete meal, or fried rice for a treat.";
            time = 120;
            ingredients = "4 teaspoons garlic, minced" + System.Environment.NewLine + "1 teaspoon ground black pepper" + System.Environment.NewLine + "4 pounds pork tenderloin" + System.Environment.NewLine + "2 teaspoons dried oregano";
            instructions = "Combine garlic, rosemary, oregano, salt, and pepper in a small bowl. Rub spice mixture all over the pork tenderloin" + System.Environment.NewLine + "Bake in the preheated oven until pork is slightly pink in the center, 20 to 25 minutes. An instant-read thermometer inserted into the center should read at least 145 degrees F (63 degrees C)";
            image = "images\\6155203.jpg";
            keywords = "pork tenderloin garlic pepper oregano";
            Recipe recipe5 = new Recipe(id, name, description, time, ingredients, instructions, image, userName, keywords);
            recipe5.AddComment("evelynj", "Great!");
            _recipes.Add(recipe5);

            id = 105;
            name = "Easy Pineapple Chicken";
            description = "An easy weeknight dinner, this quick chicken stir-fry with pineapple is as delicious as it is colorful! Serve with rice for a complete meal, or fried rice for a treat. Add red chile flakes to the mix if you want something sweet and spicy!";
            time = 20;
            ingredients = "3 tablespoons soy sauce" + System.Environment.NewLine + "1 pound boneless, skinless chicken breast, cut into strips" + System.Environment.NewLine + "1 red bell pepper, cubed" + System.Environment.NewLine + "3 tablespoons olive oil, divided";
            instructions = "Combine soy sauce, 2 tablespoons olive oil, paprika, and salt in a bowl" + System.Environment.NewLine + "Heat the remaining 1 tablespoon of olive oil in a wok. Add bell pepper and stir-fry for 3 minutes. Add scallions and cook for 2 more minutes";
            image = "images\\5151529.jpg";
            keywords = "soy sauce chicken breast bell pepper olive oil";
            Recipe recipe6 = new Recipe(id, name, description, time, ingredients, instructions, image, userName, keywords);
            _recipes.Add(recipe6);
        }
    }
}