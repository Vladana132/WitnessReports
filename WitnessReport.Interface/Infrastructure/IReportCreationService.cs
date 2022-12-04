using WitnessReports.Model.Entites;

namespace WitnessReports.Interface.Infrastructure
{
    public interface IReportCreationService
    {
        void Create(WitnessReport report);
    }
}
