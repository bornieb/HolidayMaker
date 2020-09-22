using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using HolidayMaker.Client.Model;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;

namespace HolidayMaker.Client.Service
{
     public class BookingService
     {
        private static readonly string url = "http://localhost:59571/api/booking";
        HttpClient httpClient;

        public BookingService()
        {
            httpClient = new HttpClient();
        }

        public async Task PostBooking(Booking booking)
        {
            var jsonBooking = JsonConvert.SerializeObject(booking);
            HttpContent httpContent = new StringContent(jsonBooking);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var jsonBookingDB = await httpClient.PostAsync(url, httpContent);
         
        }

        public async Task<ObservableCollection<BookedRoom>> GetBookedRooms(int accommodationId, DateTime checkIn, DateTime checkOut)
        {
            var bookedRooms = new ObservableCollection<BookedRoom>();
            var jsonBookedRooms = await httpClient.GetStringAsync(url + $"/booked?accommodationId={accommodationId}&checkIn={checkIn}&checkOut={checkOut}");
            bookedRooms = JsonConvert.DeserializeObject<ObservableCollection<BookedRoom>>(jsonBookedRooms);

            return bookedRooms;
        }

     
     }
}
