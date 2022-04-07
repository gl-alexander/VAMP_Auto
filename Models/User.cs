using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VAMPAutoCore.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Username { set; get; }

        [Required(ErrorMessage = "Required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string UCN { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public virtual ICollection<Query> Queries { set; get; }
    }
}
