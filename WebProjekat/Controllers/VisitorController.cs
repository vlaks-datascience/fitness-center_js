using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class VisitorController : ApiController
    {
        [HttpGet, Route("api/visitor/{Name}")]
        public string Get(string Name)
        {
            if (Name == "" || Name == null)
            {
                return null;
            }
            return ListGroupTraining.Dodaj(Name);
        }
        public List<GroupTraining> Get()
        {
            return ListGroupTraining.GetPreviousTrainings();
        }

        public List<GroupTraining> Get(string Name, string TrainingType, string FCName)
        {
            return ListGroupTraining.SortingPrevious(Name, TrainingType, FCName);
        }
    }
}
