using System.ComponentModel.DataAnnotations;

namespace Tour_management.Models
{
    public class Booking
    {
        [Key]
        public int book_id { get; set; }
        public int pack_id { get; set; }
        public DateTime booking_date { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
    }

    
}
