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
        private static readonly string url = "http://localhost:59571/api/booking/";
        private static readonly string bUrl = "http://localhost:59571/api/booking/all/?email=";
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

        public async Task<ObservableCollection<Booking>> GetBookingsAsync(string email)
        {
            var bookings = new ObservableCollection<Booking>();
            var jsonBookings = await httpClient.GetStringAsync(bUrl + email);
            bookings = JsonConvert.DeserializeObject<ObservableCollection<Booking>>(jsonBookings);
            return bookings;
        }

        public async Task UpdateUserBooking(BookedRoom room, string email)
        {
            var updatedBookedRoom = JsonConvert.SerializeObject(room);
            HttpContent httpContent = new StringContent(updatedBookedRoom);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //await httpClient.PutAsync( *LÄGG IN USER URL HÄR* )
        }

        public async Task DeleteUserBooking(BookedRoom room, string email)
        {
            var updatedBookedRoom = JsonConvert.SerializeObject(room);
            HttpContent httpContent = new StringContent(updatedBookedRoom);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //await httpClient.DeleteAsync( *LÄGG IN USER URL HÄR* )
        }
    }
}
