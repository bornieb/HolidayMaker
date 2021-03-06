﻿using HolidayMaker.Client.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            var accommodations = new ObservableCollection<Accommodation>();
            var jsonAccommodations = await httpClient.GetStringAsync(url);
            accommodations = JsonConvert.DeserializeObject<ObservableCollection<Accommodation>>(jsonAccommodations);

            return accommodations;
        }

    }
}
