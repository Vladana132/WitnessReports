using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WitnessReports.Api;
using WitnessReports.Common.DTO;
using Xunit;

namespace WitnessReports.IntegrationTests
{
    public class WitnessReportsIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;
        public WitnessReportsIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.Test.json");

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.Sources.Clear();

                    conf.AddJsonFile(configPath);
                });
            });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task ReportFugitive_WhenCalled_ReportIsCreated()
        {
            var dto = new WitnessReportDto
            {
                WitnessName = "Witness No1",
                FugitiveName = "ROBERT WILLIAM FISHER",
                PhoneNumber = "+38765222888"
            };

            var response = await _client.PostAsync(GetUrl(), ContentHelper.GetStringContent(dto));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReportFugitive_FugitiveDoesNotExists_NotFoundReturned()
        {
            var dto = new WitnessReportDto
            {
                WitnessName = "Witness No1",
                FugitiveName = "Fugitive No2",
                PhoneNumber = "+3810644192994"
            };

            var response = await _client.PostAsync(GetUrl(), ContentHelper.GetStringContent(dto));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ReportFugitive_PhoneNumberIsNotValid_BadRequestReturned()
        {
            var dto = new WitnessReportDto
            {
                WitnessName = "Witness No1",
                FugitiveName = "Fugitive No2",
                PhoneNumber = "32131123gff"
            };

            var response = await _client.PostAsync(GetUrl(), ContentHelper.GetStringContent(dto));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        private string GetUrl()
        {
            return $"api/WitnessReport/fugitive";
        }

        private static class ContentHelper
        {
            public static StringContent GetStringContent<T>(T obj) where T : class
               => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}
