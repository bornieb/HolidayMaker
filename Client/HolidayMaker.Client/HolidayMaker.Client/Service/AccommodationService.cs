using HolidayMaker.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolidayMaker.Client.Service
{
    public class AccommodationService
    {

        private static readonly string url = "http://localhost:59571/api/accommodation";
        HttpClient httpClient;

        public AccommodationService()
        {
            httpClient = new HttpClient();
        }

        public async Task<ObservableCollection<Accommodation>> GetAccommodationsAsync()
        {
            var jsonAccommodations = await httpClient.GetStringAsync(url);
            var accommodations = JsonConvert.DeserializeObject<ObservableCollection<Accommodation>>(jsonAccommodations);
    
            return accommodations;
        }

        //public async Task<ObservableCollection<Room>> GetAccRoomsAsync(int accId)
        //{
        //    var jsonRooms = await httpClient.GetStringAsync(url);
        //    var rooms = JsonConvert.DeserializeObject<ObservableCollection<Room>>(jsonRooms);
        //    return rooms;
        //}
    }
}
