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
    public class UserService
    {
        private static readonly string url = "http://localhost:59571/api/User";
        HttpClient httpClient;

        public UserService()
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

        public async Task<bool> LogIn(string email, string password)
        {
            var data = new { email, password };
            var jsonData = JsonConvert.SerializeObject(data);
            HttpContent httpContent = new StringContent(jsonData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PostAsync(url+"/login", httpContent);

            return response.IsSuccessStatusCode; //om allt går bra är denna true
        }
    }
}
