using PhoneNumbers;
using System;
using System.Threading.Tasks;
using WitnessReports.Common.DTO;
using WitnessReports.Interface.Infrastructure;
using WitnessReports.Interface.Services;
using WitnessReports.Model.Entites;
using WitnessReports.Model.HttpException;

namespace WitnessReports.Service.Services
{
    public class WitnessReportService : IWitnessReportService
    {
        private readonly PhoneNumberUtil _phoneUtil;
        private readonly IFBIWantedService _fbiService;
        private readonly IReportCreationService _reportService;

        public WitnessReportService(IFBIWantedService fBIWantedService, IReportCreationService reportCreationService)
        {
            _phoneUtil = PhoneNumberUtil.GetInstance();
            _fbiService = fBIWantedService;
            _reportService = reportCreationService;
        }
        public async Task ReportFugitiveAsync(WitnessReportDto witnessReportDto)
        {
            var fugitiveResult = await _fbiService.GetByNameAsync(witnessReportDto.FugitiveName);

            var total = (int)fugitiveResult.total;

            if (total <= 0)
            {
                throw new CustomNotFoundException($"Entered name: {witnessReportDto.FugitiveName} does not match to any fugitive.");
            }

            var phoneNumber = _phoneUtil.Parse(witnessReportDto.PhoneNumber, null);

            var region = _phoneUtil.GetRegionCodeForNumber(phoneNumber);

            if (!_phoneUtil.IsValidNumberForRegion(phoneNumber, region))
            {
                throw new CustomNotFoundException($"Phone number: {witnessReportDto.PhoneNumber} is not in valid format for region {region}.");
            }

            var countryCallingNumber = _phoneUtil.FormatOutOfCountryCallingNumber(phoneNumber, region);

            var title = Convert.ToString(fugitiveResult.items[0].title);
            var url = Convert.ToString(fugitiveResult.items[0].url);

            var report = new WitnessReport(
                new Witness(witnessReportDto.WitnessName, region, countryCallingNumber),
                new Fugitive(title, url)
                );

            _reportService.Create(report);
        }
    }
}
