using System.Threading.Tasks;

namespace WitnessReports.Interface.Infrastructure
{
    public interface IFBIClient
    {
        Task<dynamic> GetByNameAsync(string name);
    }
}
