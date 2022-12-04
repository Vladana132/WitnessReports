using System.Text.Json;

namespace WitnessReports.Model.HttpException
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
