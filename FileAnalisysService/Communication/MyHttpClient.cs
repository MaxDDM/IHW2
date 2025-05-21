using FileAnalisysService.Models;
using Newtonsoft.Json;

namespace FileAnalisysService.Communication
{
    public class MyHttpClient
    {
        private readonly HttpClient _httpClient;

        public MyHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<FileText>> GetFiles()
        {
            var response = await _httpClient.GetAsync("http://host.docker.internal:5002/Data");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<FileText>>(responseData);
        }
    }
}
