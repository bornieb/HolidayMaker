using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }

        public Room(int roomId, string roomType, decimal price)
        {
            RoomId = roomId;
            RoomType = roomType;
            Price = price;
        }
    }
}
