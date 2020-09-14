﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI.Model
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        public string BookingNumber { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserID { get; set; }
        public virtual User User { get; set; }
        //public List<Room> ListOfUserBookedRooms { get; set; } = new List<Room>();
    }
}
