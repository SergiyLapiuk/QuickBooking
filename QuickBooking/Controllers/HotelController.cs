using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickBooking.Models;

namespace QuickBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly QuickBookingDbContext _context;

        public HotelsController(QuickBookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            return await _context.Hotels.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Hotels.Add(hotel);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("unique constraint"))
                    {
                        ModelState.AddModelError("Location", "Готель з такою локацією вже існує.");
                        return BadRequest(ModelState);
                    }
                    throw; 
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (!HotelExists(id))
            {
                return BadRequest();
            }

            // Отримання старого запису з бази даних
            var existingHotel = await _context.Hotels.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            if (existingHotel == null)
            {
                return NotFound();
            }

            // Призначення старого Id для редагованого запису
            hotel.Id = existingHotel.Id;

            if (ModelState.IsValid)
            {
                _context.Entry(hotel).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("unique constraint"))
                    {
                        ModelState.AddModelError("Location", "Готель з такою локацією вже існує.");
                        return BadRequest(ModelState);
                    }
                    throw;
                }

                return NoContent();
            }

            return BadRequest(ModelState);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}
