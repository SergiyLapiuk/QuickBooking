using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBooking.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string TourName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int HotelRoomId { get; set; }
        public bool IncludesTransfer { get; set; }

        public HotelRoom HotelRoom { get; set; }
        public ICollection<Transfer> Transfers { get; set; }
    }
}