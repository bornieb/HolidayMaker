using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayMakerAPI.Model
{
    public class Accommodation
    {
        [Key]
        public int AccommodationID { get; set; }
        public string AccommodationName { get; set; }
        public string City { get; set; }
        public string TypeOfAccommodation { get; set; }
        public bool HasPool { get; set; }
        public bool HasEntertainment { get; set; }
        public bool HasKidsClub { get; set; }
        public bool HasRestaurant { get; set; }
        public int DistanceToBeach { get; set; }
        public int DistanceToCenter { get; set; }
        public decimal Rating { get; set; }
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
    }
}
