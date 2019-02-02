using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Script.Serialization;

namespace MyRecipes
{
    public class UserTestData : IUserData
    {
        private static bool _initialised = false;
        private static UserTestData _userData;

        private ArrayList _users = new ArrayList();
        public ArrayList Users
        {
            get { return _users; }
        }

        public static UserTestData GetUserData()
        {
            if (_initialised == false)
            {
                _userData = new UserTestData();
                _initialised = true;
            }
            return _userData;
        }

        private UserTestData()
        {
            GenerateData();
        }

        public void Login(User checkUser)
        {
            bool found = false, validate = false;

            for (int i = 0; i < _users.Count; i++)
            {
                User u = (User)_users[i];
                if (checkUser.Name == u.Name)
                {
                    found = true;
                }
                if (checkUser.Password == u.Password)
                {
                    validate = true;
                }
            }

            if (found == false)
            {
                throw new System.ApplicationException("Username has not been registered yet!");
            }
            if (found == true && validate == false)
            {
                throw new System.ApplicationException("Please enter the correct password!");
            }
            HttpContext.Current.Session["UserName"] = checkUser.Name;
        }

        public void Register(User newUser)
        {
            bool found = false;
            for (int i = 0; i < _users.Count; i++)
            {
                User u = (User)_users[i];

                if (newUser.Name == u.Name)
                {
                    found = true;
                    throw new System.ApplicationException("Username has already been registered!");
                }
            }
            if (!found)
            {
                _users.Add(newUser);
            }
        }

        public void Logout()
        {
            HttpContext.Current.Session["UserName"] = "";
        }
        
        public void GenerateData()
        {
            string userName = "linh";
            string password = "linh123";
            User user1 = new User(userName, password);
            _users.Add(user1);

            userName = "minhanh";
            password = "minhanh1";
            User user2 = new User(userName, password);
            _users.Add(user2);

            userName = "evelynj";
            password = "evelynj1";
            User user3 = new User(userName, password);
            _users.Add(user3);

            userName = "joseh";
            password = "joseh1";
            User user4 = new User(userName, password);
            _users.Add(user4);

            userName = "dtx";
            password = "dxt1";
            User user5 = new User(userName, password);
            _users.Add(user5);

            userName = "admin";
            password = "admin1";
            User user6 = new User(userName, password);
            _users.Add(user6);
        }
    }
}
