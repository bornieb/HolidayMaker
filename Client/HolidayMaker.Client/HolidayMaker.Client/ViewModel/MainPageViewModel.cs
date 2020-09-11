using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolidayMaker.Client.Model;

namespace HolidayMaker.Client.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<Accommodation> ListOfAccommodations = new ObservableCollection<Accommodation>();
        //static int bookingNumber = 1000;
        public void MockData()
        {
            ListOfAccommodations.Add(new Accommodation("Eriks Lya", "Malmö"));
            ListOfAccommodations.Add(new Accommodation("Rays Lya", "Eslöv"));
        }

        public Booking AddToBooking(Room room, Accommodation accommodation)
        {
            Booking booking = new Booking();
            BookedRoom bookedRoom = new BookedRoom();

            booking.BookingNumber = CreateBookingNumber();
            bookedRoom.AccommodationName = accommodation.AccommodationName;
            bookedRoom.City = accommodation.City;
            bookedRoom.Price = room.Price;
            bookedRoom.RoomType = room.RoomType;
            booking.BookedRooms.Add(bookedRoom);
            booking.TotalPrice = CalculateTotalPrice(booking);

            return booking;

        }

        public string CreateBookingNumber()
        {
            //bookingNumber += 1;
            //return bookingNumber;
            Random rndGenerator = new Random();
            StringBuilder myRandomString = new StringBuilder();

            for (int i = 0; i < 10; i++) // set how many random characters we want to generate
            {
                bool isAlpha = Convert.ToBoolean(rndGenerator.Next(0, 2));
                int rndNumber = isAlpha ? rndGenerator.Next(0, 26) : rndGenerator.Next(0, 10); // set the boundry 26 letters in alphabet, 10 numbers
                if (isAlpha)
                {
                    bool isUpper = Convert.ToBoolean(rndGenerator.Next(0, 2));
                    rndNumber += isUpper ? 65 : 97; // add an offset of 65 which gets us to an A in the ASCII table, 97 is same for a
                }

                myRandomString.Append(isAlpha ? ((char)rndNumber).ToString() : rndNumber.ToString());
            }

            return myRandomString.ToString();
        }

        public decimal CalculateTotalPrice(Booking booking)
        {
            decimal totalPrice = 0;
            foreach(BookedRoom room in booking.BookedRooms)
            {
                totalPrice += room.Price;
            }
            return totalPrice;
            
        }
    }
}
