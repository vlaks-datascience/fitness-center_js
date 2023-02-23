using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProjekat.Models
{
    public class Comment
    {
        public User ReviewVisitor { get; set; }
        public FitnessCenter ReviewFitnessCenter { get; set; }
        public string ReviewVisitorsComment { get; set; }
        public int ReviewRating { get; set; }
        public string Accepted { get; set; }

        public Comment()
        {
        }
    }
}