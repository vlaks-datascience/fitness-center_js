using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProjekat.Models.Enums;

namespace WebProjekat.Models
{
    public class GroupTraining
    {
        public string Name { get; set; }
        public string TrainingType { get; set; }
        public FitnessCenter FitnessCenter { get; set; }
        public double TrainingDuration { get; set; }
        public DateTime DateNTime { get; set; }
        public int MaxVisitors { get; set; }
        public List<User> VisitorsListing { get; set; }

        public GroupTraining()
        {
        }


    }
}