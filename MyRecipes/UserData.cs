using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRecipes
{
    public class UserData
    {
        public void Login(string username, string password)
        {
            if (username == "" || password == "")
            {
                throw new System.ArgumentException("Username and/or pasword must have a value!");
            }
            if (username != password)
            {
                throw new System.ApplicationException("Username and/or pasword are not correct!");
            }

            HttpContext.Current.Session["UserName"] = username;
        }

        public void Logout()
        {
            HttpContext.Current.Session["UserName"] = "";
        }
    }
}