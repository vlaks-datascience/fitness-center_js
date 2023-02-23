using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class FitnessCenter
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Established { get; set; }
        public User Owner { get; set; } 
        public double MonthCost { get; set; }
        public double YearCost { get; set; }
        public double OneCost { get; set; }
        public double OneGroupCost { get; set; }
        public double OneWithCoachCost { get; set; }

        public FitnessCenter()
        {
        }
    }
}