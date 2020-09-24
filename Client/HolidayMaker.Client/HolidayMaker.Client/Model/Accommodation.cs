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
        public int AccommodationID { get; set; }
        public string AccommodationName { get; set; }
        public string City { get; set; }
        public decimal Rating { get; set; }
        public bool HasPool { get; set; }
        public bool HasEntertainment { get; set; }
        public bool HasKidsClub { get; set; }
        public bool HasRestaurant { get; set; }
        public int DistanceToBeach { get; set; }
        public int DistanceToCenter { get; set; }
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
        public string AccRating
        {
            get
            {
                return $"Accommodation rating: {Rating}";
            }
        }

        public string AccHasPool
        {
            get
            {
                if (HasPool)
                    return $"The accommodation has a pool.";
                else
                {
                    return $"This accommodation has no pool.";
                }
            }
        }
        public string AccHasEntertainment
        {
            get
            {
                if (HasEntertainment)
                    return $"The accommodation has evening entertainment.";
                else
                {
                    return $"The accommodation does not have evening entertainment.";
                }
            }
        }
        public string AccHasKidsClub
        {
            get
            {
                if (HasKidsClub)
                    return $"The accommodation has a kid's club.";
                else
                {
                    return $"The accommodation does not have a kid's club.";
                }
            }
        }
        public string AccHasRestaurant
        {
            get
            {
                if (HasRestaurant)
                {
                    return $"The accommodation has a restaurant.";
                }
                else
                {
                    return $"The accommodation does not have a restaurant.";
                }
            }
        }
        public string AccDistanceToBeach
        {
            get
            {
                return $"Distance to beach: {DistanceToBeach} m";
            }
        }
        public string AccDistanceToCenter
        {
            get
            {
                return $"Distance to centre: {DistanceToCenter} m";
            }
        }

    }
}
