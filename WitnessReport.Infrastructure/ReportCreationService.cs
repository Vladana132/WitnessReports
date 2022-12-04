using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using WitnessReports.Interface.Infrastructure;
using WitnessReports.Model.Entites;

namespace WitnessReports.Infrastructure
{
    public class ReportCreationService : IReportCreationService
    {
        private readonly IConfiguration _configuration;
        private readonly string _reportFileLocation;
        public ReportCreationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _reportFileLocation = _configuration.GetSection("App:ReportBaseLocation")?.Value;
        }

        public void Create(WitnessReport report)
        {
            var reportContent = report.BuildReportBody();
            File.AppendAllText(GetFilePath(report.Fugitive.Name), reportContent, Encoding.UTF8);
        }

        private string GetFilePath(string folderName)
        {
            var builder = new StringBuilder(_reportFileLocation)
                .Append($@"FBI\fugitives\")
                .Append($@"{folderName}\");

            var path = builder.ToString();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path + $@"{Guid.NewGuid()}-report.txt";
        }
    }
}
