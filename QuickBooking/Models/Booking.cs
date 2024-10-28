using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string Status { get; set; }

        public User User { get; set; }
        public Tour Tour { get; set; }
    }
}