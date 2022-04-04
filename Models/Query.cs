using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMP_Auto.Models
{
    public class Query
    {
        public int QueryId { set; get; }
        public virtual Car Car { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { get; set; }
        public virtual User User { set; get; }
        public int CarId { set; get; }
        public int UserId { set; get; }

    }
}
