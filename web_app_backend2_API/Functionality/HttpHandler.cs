﻿using System.Net.Http;
using System.Threading.Tasks;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API
{
    public class HttpHandler : IHttpHandler
    {
       
        public HttpResponseMessage Get(string url, HttpClient _client)
        {
            return GetAsync(url, _client).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url, HttpClient _client)
        {
            return await _client.GetAsync(url);
        }
    }
}