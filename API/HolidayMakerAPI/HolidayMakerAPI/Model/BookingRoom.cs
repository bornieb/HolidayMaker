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
        public virtual Booking Booking { get; set; }
        public virtual Room Room { get; set; }
        //public string AccommodationName { get; set; }
        //public string City { get; set; }
        //public string RoomType { get; set; }
        //public decimal Price { get; set; }
        public bool ExtraBedBooked { get; set; }
        public bool FullBoard { get; set; }
        public bool HalfBoard { get; set; }
        public bool AllInclusive { get; set; }

    }
}
