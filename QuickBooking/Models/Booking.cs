namespace QuickBooking.Models
{
    public class Booking
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } 
        public string Status { get; set; } 
    }
}
