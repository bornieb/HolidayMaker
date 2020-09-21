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
    public class BookingController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public BookingController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
            return await _context.Booking.ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            //var booking = await _context.Booking.FindAsync(id);

            //if (booking == null)
            //{
            //    return NotFound();
            //}

            var userBooking = await _context.Booking
                .Include(b => b.BookedRooms)
                .ThenInclude(br => br.Room)
                .Select(r => new
                {
                    r.BookingID,
                    r.BookingNumber,
                    r.CheckIn,
                    r.CheckOut,
                    r.TotalPrice,
                    room = r.BookedRooms.Select(br => new
                    {
                        br.RoomID,
                        br.ExtraBedBooked,
                        br.FullBoard,
                        br.HalfBoard,
                        br.AllInclusive

                    })
                }).ToListAsync();
            return Ok(userBooking);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllUserBookings(string email)
        {
            var bookings = await _context.Booking
                .Include(b => b.BookedRooms)
                .ThenInclude(br => br.Room)
                .ThenInclude(r => r.Accommodation)
                .Where(b => b.Email == email)
                .ToListAsync();

            foreach (var booking in bookings)
            {
                foreach (var bookedRoom in booking.BookedRooms)
                {
                    bookedRoom.Booking = null;
                    bookedRoom.Room.Accommodation.Rooms = null;
                }
            }

            return bookings;
        }

        [HttpDelete("all/{email}/{bNumber}")]
        public async Task<ActionResult<Booking>> DeleteUserBooking(string bNumber)
        {
            var booking = await _context.Booking
                .Include(b => b.BookedRooms)
                .Where(b => b.BookingNumber == bNumber)
                .FirstOrDefaultAsync();
            if (booking == null)
            {
                return NotFound();
            }
            
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingID)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        [HttpPut("all/{bookingNumber}/{booking}")]
        public async Task<IActionResult> PutUserBooking(string bookingNumber, Booking booking)
        {
            var dBbooking = await _context.Booking
                            .Where(b => b.BookingNumber == bookingNumber)
                                .FirstOrDefaultAsync();

            booking.BookingID = dBbooking.BookingID;

            if (booking == null)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.BookingID))
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

        //POST: api/Booking
        //To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        //{
        //    _context.Booking.Add(booking);


        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
        //}

        //UNDER CONSTRUCTION
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            //var bookingRoom = new BookingRoom() { Booking = booking };
            //var userRooms = booking.BookedRooms.Select(r => r.RoomID).ToList();

            foreach (var bookedRoom in booking.BookedRooms)
            {
                //var bookingRoom = new BookingRoom() { BookingID = booking.BookingID, RoomID = room.RoomID };
                _context.BookingRoom.Add(bookedRoom);
            }

            //var bookingRoom = new BookingRoom() { BookingID = booking.BookingID };

            _context.Booking.Add(booking);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }

            return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingID == id);
        }
    }
}
