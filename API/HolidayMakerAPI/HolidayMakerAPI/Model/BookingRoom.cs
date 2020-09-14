using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI.Model
{
    public class BookingRoom
    {
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public bool ExtraBedBooked { get; set; }
        public bool FullBoard { get; set; }
        public bool HalfBoard { get; set; }
        public bool AllInclusive { get; set; }

    }
}
