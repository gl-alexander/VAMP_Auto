using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMP_Auto.Models
{
    public class Car
    {
        public int CarId { set; get; }
        public string Brand { set; get; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public double Price { set; get; }

        public virtual ICollection<Query> Queries { set; get; }
    }
}
