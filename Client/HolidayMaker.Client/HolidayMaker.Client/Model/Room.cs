using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class Room
    {
        public string RoomType { get; set; }
        public decimal Price { get; set; }

        public Room(string roomType, decimal price)
        {
            RoomType = roomType;
            Price = price;
        }
    }
}
