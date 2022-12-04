using System;
using System.Text;

namespace WitnessReports.Model.Entites
{
    public class WitnessReport
    {
        public Witness Witness { get; set; }
        public Fugitive Fugitive { get; set; }

        public WitnessReport(Witness witness, Fugitive fugitive)
        {
            Witness = witness;
            Fugitive = fugitive;
        }

        public string BuildReportBody()
        {
            var builder = new StringBuilder();
            builder
                .AppendLine($"Report created: {DateTime.UtcNow}")
                .AppendLine()
                .AppendLine("Witness Infromation: ")
                .AppendLine()
                .AppendLine($"Name: {Witness.Name}")
                .AppendLine($"Phone number: {Witness.PhoneNumber}")
                .AppendLine($"Country: {Witness.Country}")
                .AppendLine()
                .AppendLine("Fugitive Information:")
                .AppendLine()
                .AppendLine($"Name: {Fugitive.Name}")
                .AppendLine($"Link: {Fugitive.Link}");

            return builder.ToString();
        }
    }
}
