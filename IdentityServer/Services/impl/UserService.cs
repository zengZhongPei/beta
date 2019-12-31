using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace IdentityServer.Services.impl
{
    public class UserService : IUserService
    {
        private readonly string userServerUrl = "http://localhost:64580/";

        private readonly HttpClient _httpClient;


        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> CheckOrCreate(string phone)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new {phone}));
            var result=await _httpClient.PostAsync($"{userServerUrl}api/users/check-or-create", content);
            if (result.IsSuccessStatusCode)
            {
                var userId =await result.Content.ReadAsStringAsync();
                int.TryParse(userId, out int intUserId);
                return intUserId;
            }
            return 0;
        }
    }
}
