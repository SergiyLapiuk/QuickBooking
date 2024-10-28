using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuickBooking.Models
{
    public class RoomType
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва типу кімнати є обов'язковою.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Назва типу кімнати повинна бути від 3 до 50 символів.")]
        public string TypeName { get; set; }

        [StringLength(200, ErrorMessage = "Опис повинен мати не більше 200 символів.")]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<HotelRoom>? HotelRooms { get; set; }
    }
}
