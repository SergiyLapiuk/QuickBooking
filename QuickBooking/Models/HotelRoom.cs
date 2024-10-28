using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuickBooking.Models
{
    public class HotelRoom
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        [Required(ErrorMessage = "Номер кімнати є обов'язковим.")]
        [StringLength(10, ErrorMessage = "Номер кімнати не повинен перевищувати 10 символів.")]
        public string RoomNumber { get; set; }

        [Required]
        public int HotelId { get; set; }
        [JsonIgnore]
        public Hotel? Hotel { get; set; }
        [JsonIgnore]
        public RoomType? RoomType { get; set; }
    }
}
