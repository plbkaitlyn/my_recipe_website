using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyRecipes
{
    public class User
    {
        private string _userName;
        public string Name
        {
            get { return _userName; }
        }

        private string _userEmail;
        public string Email
        {
            get { return _userEmail; }
        }

        private string _userPassword;
        public string Password
        {
            get { return _userPassword; }
        }
        
        public User(string name, string password)
        {
            if (name == "" || password == "")
            {
                throw new System.ArgumentException("Username and/or pasword must have a value!");
            }
            _userName = name;
            _userPassword = password;
        }

        public User(string name, string email, string password)
        {
            if (name == "" || email == "" || password == "")
            {
                throw new System.ArgumentException("No field is empty!");
            }
            Regex regex = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$");
            Match match = regex.Match(email);
            if (!match.Success)
                throw new System.ApplicationException("Please provide a valid email address!");

            _userName = name;
            _userEmail = email;
            _userPassword = password;
        }
    }
}