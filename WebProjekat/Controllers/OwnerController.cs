using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class OwnerController : ApiController
    {
        public List<Comment> Get()
        {
            return ListComment.Approve();
        }

        public bool Get(string Nameu, string Namef, string Commentf)
        {
            return ListComment.Confirm(Nameu, Namef, Commentf);
        }

        [HttpGet]
        public bool GetDeny(string NameUserDeny, string NameFCDeny, string CommentDeny)
        {
            return ListComment.DenyComment(NameUserDeny, NameFCDeny, CommentDeny);
        }
    }
}
