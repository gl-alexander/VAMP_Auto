using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMP_Auto.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { set; get; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UCN { get; set; }
        public string  Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Query> Queries { set; get; }
    }
}
