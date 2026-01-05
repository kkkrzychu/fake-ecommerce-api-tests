using FakeEcommerce.ApiTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace FakeEcommerce.ApiTests.Clients
{
    public class PostsApiClient
    {
        private readonly HttpClient _client;

        public PostsApiClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
            };
        }

        public async Task<HttpResponseMessage> GetPosts()
        {
            return await _client.GetAsync("posts");
        }

        public async Task<HttpResponseMessage> GetPostById(int id)
        {
            return await _client.GetAsync($"posts/{id}");
        }

        public async Task<HttpResponseMessage> CreatePost(CreatePostRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _client.PostAsync("posts", content);
        }
    }
}