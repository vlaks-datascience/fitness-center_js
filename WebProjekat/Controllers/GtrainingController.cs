using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class GtrainingController : ApiController
    {
        public List<GroupTraining> Get()
        {
            return ListGroupTraining.LoadAllGTS();
        }
        
        public List<GroupTraining> Get(string Vreme)
        {
            return ListGroupTraining.GetAllGTS(Vreme);
        }

        [HttpGet]
        public string GetCheckTime(string TAJM)
        {
            return ListGroupTraining.CHECKTAJM(TAJM);
        }

        public IHttpActionResult Put(GroupTraining groupTraining)
        {
            return Ok(ListGroupTraining.UpdateGroupTraining(groupTraining));
        }

    }
}
