﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }

        //public List<Booking> ListOfUserBookings { get; set; } = new List<Booking>();
    }
}
