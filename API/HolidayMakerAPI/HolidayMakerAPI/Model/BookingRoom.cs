using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI.Model
{
    public class BookingRoom
    {
        [Key]
        [Column(Order = 1)]
        public int BookingID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int RoomID { get; set; }
        public Booking Booking { get; set; }
        public Room Room { get; set; }
        public bool ExtraBedBooked { get; set; }
        public bool FullBoard { get; set; }
        public bool HalfBoard { get; set; }
        public bool AllInclusive { get; set; }

    }
}
