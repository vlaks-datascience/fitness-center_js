using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class CoachController : ApiController
    {
        // svi gt za trenera
        public List<GroupTraining> Get()
        {
            return ListGroupTraining.GTforCoach();
        }

        public List<GroupTraining> Get(string Name, string TrainingType, string GTmin, string GTmax)
        {
            return ListGroupTraining.SortingPersonal(Name, TrainingType, GTmin, GTmax);
        }

        public IHttpActionResult Post(GroupTraining groupTraining)
        {
            if (groupTraining == null)
            {
                return BadRequest();
            }
            return Ok(ListGroupTraining.Register(groupTraining));
        }
    }
}
