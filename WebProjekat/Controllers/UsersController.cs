using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class UsersController : ApiController
    {
        public List<User> Get()
        {
            return ListUser.UsersList;
        }

        public User Get(string username)
        {
            return ListUser.FindByUsername(username);
        }

        public IHttpActionResult Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(ListUser.Register(user));
        }

        public IHttpActionResult Put(User user)
        {
            if (ListUser.Logger.Role == Models.Enums.UserRoles.Visitor)
            {
                return Ok(ListUser.UpdateVisitor(user));
            }
            else if(ListUser.Logger.Role == Models.Enums.UserRoles.Coach)
            {
                return Ok(ListUser.UpdateCoach(user));
            }
            else if(ListUser.Logger.Role == Models.Enums.UserRoles.Owner)
            {
                return Ok(ListUser.UpdateOwner(user));
            }
            return null;
        }

        public IHttpActionResult Delete(string username)
        {
            User user = ListUser.FindByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            ListUser.RemoveUser(user);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
