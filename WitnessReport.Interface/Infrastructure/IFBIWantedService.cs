using System.Threading.Tasks;

namespace WitnessReports.Interface.Infrastructure
{
    public interface IFBIWantedService
    {
        Task<dynamic> GetByNameAsync(string name);
    }
}
