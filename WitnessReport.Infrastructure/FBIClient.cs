using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WitnessReports.Interface.Infrastructure;

namespace WitnessReports.Infrastructure
{
    public class FBIClient : IFBIClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public FBIClient(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(_configuration.GetSection("App:FbiApiBaseUrl")?.Value);
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<dynamic> GetByNameAsync(string name)
        {
            var url = $"list?title={name}";

            using (var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject(content);

                return data;
            }
        }
    }
}
