using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI.Model
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public virtual Accommodation Accommodation { get; set; }
        public int AccommodationID { get; set; }


    }
}
