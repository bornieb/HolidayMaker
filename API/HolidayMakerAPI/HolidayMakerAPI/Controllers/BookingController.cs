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

        //[HttpGet("a/{userId}")]
        //public async Task<ActionResult<IEnumerable<Booking>>> GetAllUserBookings(string email)
        ////{
        ////    var u = await _context.User
        ////        .Include(u => u.ListOfUserBookings)
        ////        .ThenInclude(b => b.BookedRooms)
        ////        .ThenInclude(r => r.Room)
        ////        .ThenInclude(a => a.Accommodation)
        ////        .Where(u => u.UserID == userId)
        ////        .FirstOrDefaultAsync();

        ////    u.ListOfUserBookings.ForEach(b => b.User = null);

        ////    return u.ListOfUserBookings;

        //    //var userBookings = new
        //    //    {
        //    //        u.FirstName,
        //    //        u.LastName,
        //    //        u.Email,
        //    //        booking = u.ListOfUserBookings.Select(b => new
        //    //        {
        //    //            b.BookingID,
        //    //            b.BookingNumber,
        //    //            b.CheckIn,
        //    //            b.CheckOut,
        //    //            b.TotalPrice,
        //    //            room = b.BookedRooms.Select(r => new
        //    //            {
        //    //                r.RoomID,
        //    //                r.ExtraBedBooked,
        //    //                r.FullBoard,
        //    //                r.HalfBoard,
        //    //                r.AllInclusive
        //    //            })
        //    //        })
        //    //    };

        //    //return Ok(userBookings);
        //}

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
