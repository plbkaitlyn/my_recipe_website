using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRecipes
{
    public class UserComment
    {
        private string _username;
        public string UserName
        {
            get { return _username; }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
        }

        public UserComment(string username, string comment)
        {
            _username = username;
            _comment = comment;
        }
    }
}