using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickBooking.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly QuickBookingDbContext _context;

        public HotelRoomController(QuickBookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms()
        {
            return await _context.HotelRooms
                .Include(hr => hr.Hotel)
                .Include(hr => hr.RoomType)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int id)
        {
            var hotelRoom = await _context.HotelRooms
                .Include(hr => hr.Hotel)
                .Include(hr => hr.RoomType)
                .FirstOrDefaultAsync(hr => hr.Id == id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom([FromBody] HotelRoom hotelRoom)
        {
            // Перевірка моделі
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.HotelRooms.Add(hotelRoom);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetHotelRoom), new { id = hotelRoom.Id }, hotelRoom);
            }
            catch (DbUpdateException ex)
            {
                // Обробка виключення при збереженні до бази даних
                if (ex.InnerException != null && ex.InnerException.Message.Contains("unique constraint"))
                {
                    return Conflict(new { message = "Кімната з таким номером вже існує в цьому готелі." });
                }
                return StatusCode(500, new { message = "Виникла помилка при додаванні кімнати.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom(int id, HotelRoom hotelRoom)
        {
            if (!HotelRoomExists(id))
            {
                return BadRequest();
            }
            hotelRoom.Id = id;
            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelRoomExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("unique constraint"))
                {
                    return Conflict(new { message = "Кімната з таким номером вже існує в цьому готелі." });
                }
                return StatusCode(500, new { message = "Не вдалося оновити кімнату.", details = ex.Message });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelRoom(int id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);
            if (hotelRoom == null)
            {
                return NotFound(new { message = "Кімната не знайдена." });
            }

            // Видалення кімнати
            _context.HotelRooms.Remove(hotelRoom);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Не вдалося видалити кімнату.", details = ex.Message });
            }

            return NoContent(); // Повертаємо 204 No Content при успішному видаленні
        }


        private bool HotelRoomExists(int id)
        {
            return _context.HotelRooms.Any(e => e.Id == id);
        }
    }
}
