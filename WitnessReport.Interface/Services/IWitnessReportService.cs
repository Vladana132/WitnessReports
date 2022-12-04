using System.Threading.Tasks;
using WitnessReports.Common.DTO;

namespace WitnessReports.Interface.Services
{
    public  interface IWitnessReportService
    {
        Task ReportFugitiveAsync(WitnessReportDto dto);
    }
}
