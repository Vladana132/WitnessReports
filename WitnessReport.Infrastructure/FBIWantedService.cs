using Polly;
using Polly.Registry;
using System.Threading.Tasks;
using WitnessReports.Interface.Infrastructure;

namespace WitnessReports.Infrastructure
{
    public class FBIWantedService : IFBIWantedService
    {
        private readonly IFBIClient _fbiClient;
        private readonly IAsyncPolicy _asyncPolicy;


        public FBIWantedService(IFBIClient fbiClient, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            _fbiClient = fbiClient;
            _asyncPolicy = policyRegistry.Get<IAsyncPolicy>("HttpRequestPolicy");
        }

        public async Task<dynamic> GetByNameAsync(string name)
        {
            return await _asyncPolicy.ExecuteAsync(async () =>
            {
                var response = await _fbiClient.GetByNameAsync(name);

                return response;
            });
        }
    }
}
