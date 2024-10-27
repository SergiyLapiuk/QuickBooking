namespace QuickBooking.Models
{
    public class HotelRoom
    {
        public int Id { get; set; } 
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
