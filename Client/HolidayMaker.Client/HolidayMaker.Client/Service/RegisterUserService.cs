using HolidayMaker.Client.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Service
{
    public class RegisterUserService
    {
        private static readonly string url = "http://localhost:59571/api/User";
        HttpClient httpClient;

        public RegisterUserService()
        {
            httpClient = new HttpClient();
        }

        public async Task PostRegisterUser(User user)
        {
            var jsonUser = JsonConvert.SerializeObject(user);
            HttpContent httpContent = new StringContent(jsonUser);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var jsonUserDB = await httpClient.PostAsync(url, httpContent);
            var response = await jsonUserDB.Content.ReadAsStringAsync();
        }
    }
}
