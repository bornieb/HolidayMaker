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
        BookingService bookingService = new BookingService();
        public ObservableCollection<Booking> ListOfUserBookings = new ObservableCollection<Booking>();


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
            //booking.UserId = 1;
            booking.BookedRooms = listOfUserBookedRooms;

            ListOfUserBookings.Add(booking);
        }

        public async Task GetBookingsAsync()
        {
            var bookings = await bookingService.GetBookingsAsync("bajskorv.se");
            foreach (Booking item in bookings)
            {
                ListOfUserBookings.Add(item);
            }
        }

        public async Task DeleteBookingAsync(Booking b)
        {
            await bookingService.DeleteUserBookingAsync(b);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await bookingService.UpdateUserBookingAsync(booking);
        }

        //Info printed when removing a booking
        public static string PrintBookingInfo(Booking booking)
        {

            string info =
            $"BookingNumber: {booking.BookingNumber} \n" +
            $"E-mail: {booking.Email}\n" +
            $"Total price: {booking.TotalPrice}";

            return info;

        }


        //Info printed when updating a booking
        public static string PrintUpdatedInfo(Booking booking)
        {

            string addons = "\n";
            int roomNumber = 0;

            foreach (var item in booking.BookedRooms)
            {
                roomNumber++; 

                addons += $"\nRoom {roomNumber} \n";
                
                if (item.ExtraBedBooked)
                {
                    addons += "Extra bed \n";
                }

                if (item.AllInclusive)
                {
                    addons += "Allinclusive \n";
                }
                else if (item.FullBoard)
                {
                    addons += "Fullboard \n";
                }
                else if (item.HalfBoard)
                {
                    addons += "Halfboard \n";
                }
            }

            string info =
            $"BookingNumber:{booking.BookingNumber} \n" +
            $"E-mail: {booking.Email}\n" +
            $"Number of rooms in booking: {booking.BookedRooms.Count}\n" +
            $"Addons: {addons}\n" +
            $"Total price: {booking.TotalPrice}";

            return info;

        }
    }

}
