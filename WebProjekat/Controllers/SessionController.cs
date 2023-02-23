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
    public class SessionController : ApiController
    {
        public User Get()
        {
            User check = (User)HttpContext.Current.Session["user"];
            if (check == null)
            {
                return new User() { Name = null };
            }
            return check;
        }
    }
}
