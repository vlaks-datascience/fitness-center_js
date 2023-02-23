using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class CommentsController : ApiController
    {
        public List<Comment> Get()
        {
            return ListComment.LoadAllCOM();
        }

        public List<Comment> Get(string Name, string Address)
        {
            return ListComment.GetFitnessByNameAddress(Name, Address);
        }

        public bool Get(string Komentar, string Ocena, string Naziv, string NazivFC)
        {
            return ListComment.LeaveComment(Komentar, Ocena, Naziv, NazivFC);
        }

    }
}
