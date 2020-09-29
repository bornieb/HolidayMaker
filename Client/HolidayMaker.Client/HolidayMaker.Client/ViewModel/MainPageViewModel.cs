using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HolidayMaker.Client.Model;
using HolidayMaker.Client.Service;
using Windows.UI.Popups;

namespace HolidayMaker.Client.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public BookingService bookingService = new BookingService();
        public AccommodationService accommodationService = new AccommodationService();
        public ObservableCollection<Accommodation> ListOfAccommodations = new ObservableCollection<Accommodation>();
        public ObservableCollection<Accommodation> SearchResult = new ObservableCollection<Accommodation>();
        public ObservableCollection<BookedRoom> AddedRooms = new ObservableCollection<BookedRoom>();
        public ObservableCollection<Room> availableRooms = new ObservableCollection<Room>();
        public ObservableCollection<Booking> ListOfUserBookings = new ObservableCollection<Booking>();
        public event PropertyChangedEventHandler PropertyChanged;
        public decimal TotalPrice = 0;
        private User user;
             
        public async void GetAccommodationsAsync()
        {
            try
            {
                var accommodations = await accommodationService.GetAccommodationsAsync();
                foreach (Accommodation item in accommodations)
                {
                    ListOfAccommodations.Add(item);
                }
            }
            catch (System.Net.Http.HttpRequestException)
            {
                await new MessageDialog("Failed to load API").ShowAsync();

            }
        }

        public async Task GetAvailableRoomsAsync(Accommodation accommodation, DateTime checkIn, DateTime checkOut)
        {
            availableRooms.Clear();
            if (accommodation == null) return;

            var bookedRooms = await bookingService.GetBookedRoomsAsync(accommodation.AccommodationID, checkIn, checkOut);

            IEnumerable<Room> availableEnumerable = accommodation.Rooms.Where((item) => !bookedRooms.Any((item2) => item.RoomId == item2.RoomId));
            foreach (var room in availableEnumerable)
            {
                availableRooms.Add(room);
            }
        }

        public void AddToBooking(Room room, Accommodation accommodation)
        {
           
            BookedRoom bookedRoom = new BookedRoom();

            bookedRoom.AccommodationName = accommodation.AccommodationName;
            bookedRoom.City = accommodation.City;
            bookedRoom.Price = room.Price;
            bookedRoom.RoomType = room.RoomType;
            bookedRoom.RoomId = room.RoomId;
            bookedRoom.ExtraBedBooked = room.ExtraBedBooked;
            bookedRoom.FullBoard = room.FullBoard;
            bookedRoom.HalfBoard = room.HalfBoard;
            bookedRoom.AllInclusive = room.AllInclusive;
            AddedRooms.Add(bookedRoom);
            CalculateTotalPrice();
        }

        public async Task CreateBookingAsync(DateTime checkIn, DateTime checkOut)
        {
            Booking booking = new Booking();
            booking.BookingNumber = CreateBookingNumber();
            booking.CheckIn = checkIn;
            booking.CheckOut = checkOut;
            booking.BookedRooms = AddedRooms;
            booking.TotalPrice = booking.TotalPriceBooking;
            booking.Email = User.Email;
            
            await PostBookingAsync(booking);
        }
        
        public async Task PostBookingAsync(Booking booking)
        {
            await bookingService.PostBookingAsync(booking);
            await DisplayMessage($"Booking with bookingnumber: {booking.BookingNumber} created!");
        }

        public string CreateBookingNumber()
        {
            Random rndGenerator = new Random();
            StringBuilder myRandomString = new StringBuilder();

            for (int i = 0; i < 10; i++) 
            {
                bool isAlpha = Convert.ToBoolean(rndGenerator.Next(0, 2));
                int rndNumber = isAlpha ? rndGenerator.Next(0, 26) : rndGenerator.Next(0, 10); 
                if (isAlpha)
                {
                    bool isUpper = Convert.ToBoolean(rndGenerator.Next(0, 2));
                    rndNumber += isUpper ? 65 : 97; 
                }

                myRandomString.Append(isAlpha ? ((char)rndNumber).ToString() : rndNumber.ToString());
            }

            return myRandomString.ToString();
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = 0;
            foreach (BookedRoom room in AddedRooms)
            {
                TotalPrice += room.Price;
            }
        }

        public void SearchFunction(string search)
        {
            SearchResult.Clear();

            if (search == "")
            {
                SearchResult.Clear();
            }
            else
            {
                foreach (var s in ListOfAccommodations)
                {
                    if (s.AccommodationName.ToLower().Contains(search.ToLower())
                        || s.City.ToLower().Contains(search.ToLower()))
                    {
                        SearchResult.Add(s);
                    }
                }
            }
        }

        public User User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
                NotifyPropertyChanged("Email");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task DisplayMessage(string message)
        {
            await new MessageDialog(message).ShowAsync();
        }

        #region MyBookingsViewModel
        public async void GetUserBookings(string email)
        {
            var bookings = await bookingService.GetBookingsAsync(email);
            foreach (Booking item in bookings)
            {
                ListOfUserBookings.Add(item);
            }
        }

        public async Task DeleteBooking(Booking b)
        {
            await bookingService.DeleteUserBookingAsync(b);
        }

        public async Task UpdateBooking(Booking booking)
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
        #endregion  
    }
}

