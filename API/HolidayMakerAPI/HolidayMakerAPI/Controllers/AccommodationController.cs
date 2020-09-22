using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI.Data;
using HolidayMakerAPI.Model;

namespace HolidayMakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public AccommodationController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/Accommodation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accommodation>>> GetAccommodation()
        {
            var result = await _context.Accommodation.Include(r => r.Rooms)//RoomList
            .Select(r => new
            {
                r.AccommodationID,
                r.AccommodationName,
                r.City,
                r.Rating,
                Rooms = r.Rooms.Select
                (ac => new
                {
                    ac.RoomID,
                    ac.RoomNumber,
                    ac.RoomType,
                    ac.IsAvailable,
                    ac.Price,
                }
                )
            }).ToListAsync();

            return Ok(result);
            //return await _context.Accommodation.ToListAsync();
        }

        // GET: api/Accommodation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accommodation>>> GetAccommodationInpuDate()
        {

            //Bara Test för Date
            DateTime checkinD = new DateTime(2020, 9, 18);
            DateTime checkoutD = new DateTime(2020, 9, 24);

            var notBooked = await _context.Booking
                .Include(b => b.BookedRooms)
                .ThenInclude(br => br.Room)
                .ThenInclude(r => r.Accommodation)
                .Where(b => !(checkinD >= b.CheckIn && checkinD <= b.CheckOut || checkoutD >= b.CheckIn && checkoutD <= b.CheckOut))
                .ToListAsync();
            //Bara test för datetime

            var result = await _context.Accommodation.Include(r => r.Rooms)//RoomList
            .Select(r => new
            {
                r.AccommodationID,
                r.AccommodationName,
                r.City,
                r.Rating,
                Rooms = r.Rooms.Select
                (ac => new
                {
                    ac.RoomID,
                    ac.RoomNumber,
                    ac.RoomType,
                    ac.IsAvailable,
                    ac.Price,
                }
                )
            }).ToListAsync();

            return Ok(result);
            //return await _context.Accommodation.ToListAsync();



        }



        // GET: api/Accommodation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accommodation>> GetAccommodation(int id)
        {
            var accommodation = await _context.Accommodation.FindAsync(id);

            if (accommodation == null)
            {
                return NotFound();
            }

            return accommodation;
        }

        // PUT: api/Accommodation/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccommodation(int id, Accommodation accommodation)
        {
            if (id != accommodation.AccommodationID)
            {
                return BadRequest();
            }

            _context.Entry(accommodation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccommodationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accommodation
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Accommodation>> PostAccommodation(Accommodation accommodation)
        {
            _context.Accommodation.Add(accommodation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccommodation", new { id = accommodation.AccommodationID }, accommodation);
        }

        // DELETE: api/Accommodation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Accommodation>> DeleteAccommodation(int id)
        {
            var accommodation = await _context.Accommodation.FindAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            _context.Accommodation.Remove(accommodation);
            await _context.SaveChangesAsync();

            return accommodation;
        }

        private bool AccommodationExists(int id)
        {
            return _context.Accommodation.Any(e => e.AccommodationID == id);
        }
    }
}
