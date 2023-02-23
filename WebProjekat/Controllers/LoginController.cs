using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class LoginController : ApiController
    {
        public User Get(string Username, string Password)
        {
            User Logovan = ListUser.LoginCheck(Username, Password);
            ListUser.Logger = (User)HttpContext.Current.Session["user"];
            if(ListUser.Logger == null)
            {
                HttpContext.Current.Session["user"] = Logovan;
                ListUser.Logger = Logovan;
            }
            return ListUser.Logger;
        }

        public User Get(string Username)
        {
            return ListUser.Logger;
        }

        public string Get()
        {
            User LoggedIn = (User)HttpContext.Current.Session["user"];
            if (LoggedIn != null)
            {
                HttpContext.Current.Session.Abandon();
            }
            return "Success";
        }
    }
}
