using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class Booking
    {
        public string BookingNumber { get; set; } //ändrat till int för att göra det lättare i början
        public decimal TotalPrice { get; set; }

        public ObservableCollection<BookedRoom> BookedRooms { get; set; }

        public Booking()
        {
            BookedRooms = new ObservableCollection<BookedRoom>();
        }

    }
}
