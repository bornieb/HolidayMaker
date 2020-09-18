using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class Accommodation
    {
        public string AccommodationName { get; set; }
        public string City { get; set; }
        public decimal Rating { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public Accommodation(string accommodationName, string city, decimal rating)
        {
            AccommodationName = accommodationName;
            City = city;
            Rating = rating;
            Rooms = new ObservableCollection<Room>();
        }

        public Accommodation()
        {
            Rooms = new ObservableCollection<Room>();
        }
    }
}
