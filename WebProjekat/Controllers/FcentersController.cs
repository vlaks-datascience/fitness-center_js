using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebProjekat.Models;

namespace WebProjekat.Controllers
{
    public class FcentersController : ApiController
    {
        public List<FitnessCenter> Get()
        {
            return ListFitnessCenter.LoadAllFCS();
        }

        [HttpGet]
        public FitnessCenter GetNameBrale(string NameBrale)
        {
            return ListFitnessCenter.FindByName(NameBrale);
        }
        
        [HttpGet]
        public List<FitnessCenter> GetAddress(string Address)
        {
            return ListFitnessCenter.FindByAddress(Address);
        }
        
        [HttpGet]
        public List<FitnessCenter> GetNames(string Name)
        {
            return ListFitnessCenter.FindByNames(Name);
        }
        

        public List<FitnessCenter> Get(int Established)
        {
            return ListFitnessCenter.FindByEstablished(Established);
        }

        public FitnessCenter GetNameAddressSINGLE(string NameDET, string AddressDET)
        {
            return ListFitnessCenter.FindByNameAddressSINGLE(NameDET, AddressDET);
        }

        public List<FitnessCenter> GetNameAddress(string Name, string Address)
        {
            return ListFitnessCenter.FindByNameAddress(Name, Address);
        }

        [HttpGet]
        public List<FitnessCenter> GetNameEstablished(string Name, int Established)
        {
            return ListFitnessCenter.FindByNameEstablished(Name, Established);
        }

        [HttpGet]
        public List<FitnessCenter> GetAddressEstablished(string Address, int Established)
        {
            return ListFitnessCenter.FindByAddressEstablished(Address, Established);
        }



        public List<FitnessCenter> Get(string Name, string Address, int Established)
        {
            if(Name == "" && Address == "")
            {
                return ListFitnessCenter.FindByEstablished(Established);
            } else if(Name == "" && Established.ToString() == "")
            {
                return ListFitnessCenter.FindByAddress(Address);
            } else if(Address == "" && Established.ToString() == "")
            {
                return ListFitnessCenter.FindByNames(Name);
            } else
            {
                return ListFitnessCenter.FindByAll(Name, Address, Established);
            }
        }


        public IHttpActionResult Post(FitnessCenter fitnessCenter)
        {
            if (fitnessCenter == null)
            {
                return BadRequest();
            }
            if (fitnessCenter.Name == null || fitnessCenter.Name.Length == 0)
            {
                return BadRequest();
            }
            if (fitnessCenter.Address == null || fitnessCenter.Address.Length == 0)
            {
                return BadRequest();
            }
            //return Ok(ListFitnessCenter.AddFcenter(fitnessCenter));
            return BadRequest(); // treba prepravka
        }

        public IHttpActionResult Put(FitnessCenter fitnessCenter)
        {
            /*
            if (user == null)
            {
                return BadRequest();
            }
            if (user.Username == null || user.Username.Length == 0)
            {
                return BadRequest();
            }
            if (user.Password == null || user.Password.Length == 0)
            {
                return BadRequest();
            }
            if (ListUser.FindByUsername(user.Username) == null)
            {
                return BadRequest();
            }
            */
            //return Ok(ListFitnessCenter.UpdateFcenter(fitnessCenter));
            return BadRequest(); // treba prepravka
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
