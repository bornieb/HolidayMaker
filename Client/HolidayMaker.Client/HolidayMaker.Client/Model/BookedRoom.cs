using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class BookedRoom
    {
        public string AccommodationName { get; set; }
        public string City { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }

        public BookedRoom(string accommodationName, string city, string roomType, decimal price)
        {
            AccommodationName = accommodationName;
            City = city;
            RoomType = roomType;
            Price = price;
        }
        public BookedRoom()
        {

        }

    }
}
