namespace Tour_management.Models
{
    public class BookTrip
    {
        public int book_id { get; set; }
        public int pack_id { get; set; }
        public int user_id { get; set; }
        public DateTime booking_date { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
        public string nameOfPackage { get; set; }
        public int duration { get; set; }
        public int price { get; set; }
        public int persons { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        
        
    }
}
