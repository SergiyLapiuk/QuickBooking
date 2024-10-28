using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBooking.Models
{
    public class TransferType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public ICollection<Transfer> Transfers { get; set; } 
    }
}
