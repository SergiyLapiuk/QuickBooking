namespace QuickBooking.Models
{
    public class Tour
    {
        public int Id { get; set; } 
        public string TourName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; } 
        public string Duration { get; set; }
        public int HotelRoomId { get; set; }
        public bool IncludesTransfer { get; set; }
    }
}
