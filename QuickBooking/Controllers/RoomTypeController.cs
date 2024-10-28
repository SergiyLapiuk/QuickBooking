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
    public class RoomTypeController : ControllerBase
    {
        private readonly QuickBookingDbContext _context;

        public RoomTypeController(QuickBookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomType>>> GetRoomTypes()
        {
            return await _context.RoomTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomType>> GetRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }

            return roomType;
        }

        [HttpPost]
        public async Task<ActionResult<RoomType>> PostRoomType(RoomType roomType)
        {
            try
            {
                _context.RoomTypes.Add(roomType);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRoomType), new { id = roomType.Id }, roomType);
            }
            catch (DbUpdateException ex)
            {
                // Обробка виключення при збереженні до бази даних
                return Conflict(new { message = "Тип кімнати з такою назвою вже існує.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomType(int id, RoomType roomType)
        {
            // Перевірка, чи існує тип кімнати
            if (!RoomTypeExists(id))
            {
                return NotFound(new { message = "Тип кімнати не знайдено." });
            }

            // Присвоєння Id, щоб зберегти старий
            roomType.Id = id;
            _context.Entry(roomType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomTypeExists(id))
                {
                    return NotFound(new { message = "Тип кімнати не знайдено." });
                }
                throw; // Продовжуємо, якщо виникає інша помилка
            }
            catch (DbUpdateException ex)
            {
                // Обробка унікальних обмежень
                if (ex.InnerException != null && ex.InnerException.Message.Contains("unique constraint"))
                {
                    return Conflict(new { message = "Не вдалося оновити тип кімнати.", details = ex.Message });
                }
                return StatusCode(500, new { message = "Не вдалося оновити тип кімнати.", details = ex.Message });
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomTypeExists(int id)
        {
            return _context.RoomTypes.Any(e => e.Id == id);
        }
    }
}
