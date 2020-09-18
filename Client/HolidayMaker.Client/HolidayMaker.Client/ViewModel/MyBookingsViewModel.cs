using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolidayMaker.Client.Model;
using HolidayMaker.Client.Service;

namespace HolidayMaker.Client.ViewModel
{
    
    public class MyBookingsViewModel
    {
        public ObservableCollection<Booking> listOfUserBookings = new ObservableCollection<Booking>();
        

        public void Mockdata()
        {
            ObservableCollection<BookedRoom> listOfUserBookedRooms = new ObservableCollection<BookedRoom>();
            BookedRoom bookedroom = new BookedRoom();
            bookedroom.AccommodationName = "Rays lya";
            bookedroom.City = "Malmö";
            bookedroom.RoomType = "Svit";
            bookedroom.Price = 1000;

            listOfUserBookedRooms.Add(bookedroom);

            Booking booking = new Booking();
            booking.BookingNumber = "101";
            booking.CheckIn = DateTime.Now;
            booking.CheckOut = DateTime.Now;
            booking.TotalPrice = 1500;
            booking.UserId = 1;
            booking.BookedRooms = listOfUserBookedRooms;

            listOfUserBookings.Add(new Booking());
        }
    }
}
