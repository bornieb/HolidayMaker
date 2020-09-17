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
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }

        public Room(int roomId, int roomNumber, string roomType, bool isAvailable, decimal price)
        {
            RoomId = roomId;
            RoomNumber = roomNumber;
            RoomType = roomType;
            IsAvailable = isAvailable;
            Price = price;
        }
        public Room()
        {

        }
    }
}
