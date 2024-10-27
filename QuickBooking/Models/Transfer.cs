namespace QuickBooking.Models
{
    public class Transfer
    {
        public int Id { get; set; } 
        public int TourId { get; set; }
        public int TransferTypeId { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
    }
}
