using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRecipes
{
    public class Recipe
    {
        
        private int _id;
        public int Id
        {
            get { return _id; }
        }
        
        private string _name;
        public string Name 
        {
            get { return _name;  }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
        }

        private int _time;
        public int Time
        {
            get { return _time; }
        }

        private string _ingredients;
        public string Ingredients
        {
            get { return _ingredients; }
        }

        private string _instructions;
        public string Instructions
        {
            get { return _instructions; }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
        }

        private string _keywords;
        public string Keywords
        {
            get { return _keywords; }
        }

        private ArrayList _userComments = new ArrayList();
        public ArrayList UserComments
        {
            get { return _userComments; }
        }

        public Recipe() { }

        public Recipe(int id, string name, string description, string image)
        {
            if (name == null || name == "")
            {
                throw new System.ArgumentException("Recipe's name must have a value.");
            }
            if (description == null || description == "")
            {
                throw new System.ArgumentException("Recipe's description must have a value.");
            }
            if (image == null || image == "")
            {
                throw new System.ArgumentException("Recipe's image must have a value.");
            }
            _id = id;
            _name = name;
            _description = description;
            _image = image;
        }

        public Recipe(int id, string name, string description, int time, string ingredients, string instructions, string image, string username, string keywords)
        {
            if (name == null || name == "")
            {
                throw new System.ArgumentException("Recipe's name must have a value.");
            }
            if (description == null || description == "")
            {
                throw new System.ArgumentException("Recipe's description must have a value.");
            }
            
            if (description.Length < 100)
            {
                throw new System.ArgumentException("Recipe's description must have at least 100 characters.");
            }

            if (time == 0)
            {
                throw new System.ArgumentException("Recipe's cook time must be greater than 0.");
            }
            if (ingredients == null || ingredients == "")
            {
                throw new System.ArgumentException("Recipe's ingredients must have a value.");
            }
            if (instructions == null || instructions == "")
            {
                throw new System.ArgumentException("Recipe's instructions must have a value.");
            }
            if (image == null || image == "")
            {
                throw new System.ArgumentException("Recipe's image must have a value.");
            }
            _id = id;
            _name = name; 
            _description = description;
            _time = time;
            _ingredients = ingredients;
            _instructions = instructions;
            _image = image;
            _username = username;
            _keywords = keywords;
        }

        public void AddComment(string username, string comment)
        {
            if (comment == null || comment == "")
            {
                throw new System.ArgumentException("Comment cannot be blank.");
            }
            _userComments.Add(new UserComment(username, comment));
        }
    }
}