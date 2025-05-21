using APIGateway.Models;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace APIGateway.Communication
{
    public class MyHttpClient
    {
        private readonly HttpClient _httpClient;

        public MyHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> UniquenessCheck(string Text)
        {
            var response = await _httpClient.GetAsync($"http://host.docker.internal:5001/Analitics?text={Text}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }

        public async Task<string> AddFile(FileText file)
        {
            string json = JsonSerializer.Serialize(file);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://host.docker.internal:5002/File", content);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }

        public async Task<FileText> GetFile(int id)
        {
            var response = await _httpClient.GetAsync($"http://host.docker.internal:5002/File/{id}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FileText>(responseData);
        }
    }
}
