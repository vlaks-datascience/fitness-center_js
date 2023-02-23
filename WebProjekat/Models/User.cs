using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjekat.Models.Enums;

namespace WebProjekat.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public TypeSex Sex { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public UserRoles Role { get; set; }
        public List<GroupTraining> GroupTrainingsVisitor { get; set; }
        public List<GroupTraining> GroupTrainingsCoach { get; set; }
        public FitnessCenter FitnessCenterCoach { get; set; }
        public List<FitnessCenter> FitnessCenterOwner{ get; set; }

        public User()
        {

        }
    }
}