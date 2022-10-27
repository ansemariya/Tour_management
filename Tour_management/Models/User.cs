using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tour_management.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        [DisplayName("Confirm Password")]
        [Compare("password")]
        public string Confirmpassword { get; set; }
        public string email { get; set; }
        public int mobile { get; set; }
        public DateTime dob { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }

    }
}
