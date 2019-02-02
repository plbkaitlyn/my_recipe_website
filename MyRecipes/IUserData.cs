using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRecipes
{
    public interface IUserData
    {
        void Login(User checkUser);
        void Register(User newUser);
        void Logout();
    }
}