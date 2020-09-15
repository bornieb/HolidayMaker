﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolidayMaker.Client.Model;
using HolidayMaker.Client.Service;

namespace HolidayMaker.Client.ViewModel
{
    public class MainPageViewModel
    {
        public BookingService bookingService = new BookingService();

        public ObservableCollection<Accommodation> ListOfAccommodations = new ObservableCollection<Accommodation>();
        public ObservableCollection<Accommodation> SearchResult = new ObservableCollection<Accommodation>();

        public ObservableCollection<BookedRoom> AddedRooms = new ObservableCollection<BookedRoom>();
        public decimal TotalPrice = 0;


        public void MockData()
        {
            ListOfAccommodations.Add(new Accommodation("Erics Lya", "Malmö", 1.7m));
            ListOfAccommodations.Add(new Accommodation("Rays Lya", "Eslöv", 0.2m));
            ListOfAccommodations.Add(new Accommodation("Mickes hak", "Hjärup", 2.5m));
            ListOfAccommodations.Add(new Accommodation("Jennys Etage", "Los Angeles", 4.9m));
            ListOfAccommodations.Add(new Accommodation("Glenns koja", "Vardagsrummet", 5m));
        }


        public void AddToBooking(Room room, Accommodation accommodation)
        {
           
            BookedRoom bookedRoom = new BookedRoom();

            bookedRoom.AccommodationName = accommodation.AccommodationName;
            bookedRoom.City = accommodation.City;
            bookedRoom.Price = room.Price;
            bookedRoom.RoomType = room.RoomType;
            AddedRooms.Add(bookedRoom);
            CalculateTotalPrice();
            //booking.BookedRooms.Add(bookedRoom);
            //booking.TotalPrice = CalculateTotalPrice(booking);

            //return booking;

        }

        public async void CreateBooking()
        {
            Booking booking = new Booking();
            booking.BookingNumber = CreateBookingNumber();
            booking.CheckIn = DateTime.Now;
            booking.CheckOut = DateTime.Now;
            booking.TotalPrice = TotalPrice;
            booking.BookedRooms = AddedRooms;
            //AddedRooms.Clear();
            await PostBookingAsync(booking);
        }

        public async Task PostBookingAsync(Booking booking)
        {
            await bookingService.PostBooking(booking);
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

        public void CalculateTotalPrice()
        {
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
                        SearchResult.Add(new Accommodation(s.AccommodationName, s.City, s.Rating));
                    }
                }
            }
        }

        public void SortingFunction()
        {
            var sorted = SearchResult.OrderByDescending(x => x.Rating);

        }
    }
}

