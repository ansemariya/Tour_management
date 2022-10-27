using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Tour_management.Models
{
    public class Package
    {
        [Key]
        public int pack_id { get; set; }
        [Required]
        public string nameOfPackage { get; set; }
        //public string? pack_type { get; set; }
        public int duration { get; set; }
        public int price { get; set; }
        public string description{ get; set; }
        public int persons { get; set; }
        public string destination1 { get; set; }
        public string? destination2 { get; set; }
        public string? destination3 { get; set; }

        public string? coverpicture { get; set; }
        
    }
    
}
