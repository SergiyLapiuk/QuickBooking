using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuickBooking.Models
{
    public class Hotel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва готелю є обов'язковою.")]
        [StringLength(100, ErrorMessage = "Назва готелю повинна мати не більше 100 символів.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Локація є обов'язковою.")]
        [StringLength(200, ErrorMessage = "Локація повинна мати не більше 200 символів.")]
        public string Location { get; set; }

        [Range(0, 5, ErrorMessage = "Рейтинг повинен бути між 0 та 5.")]
        public double? Rating { get; set; }

        [Required(ErrorMessage = "Опис є обов'язковим.")]
        [StringLength(500, ErrorMessage = "Опис повинен мати не більше 500 символів.")]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<HotelRoom>? HotelRooms { get; set; }
    }
}
