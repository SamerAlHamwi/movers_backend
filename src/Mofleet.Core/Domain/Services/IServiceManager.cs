using Abp.Domain.Services;
using Mofleet.Domain.services.Dto;
using Mofleet.Domain.Services.Dto;
using Mofleet.Domain.ServiceValueForOffers;
using Mofleet.Domain.ServiceValues.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.services
{
    public interface IServiceManager : IDomainService
    {
        // Task<bool> CheckServicesIsCorrect(List<ServiceValuesDto> serviceValues);
        //public Task<ServiceDetailsDto> GetServiceByRequestForQuotationIdAsync(long requestId);
        Task<bool> CheckServiceIfExict(List<ServiceTranslationDto> translations);
        Task<ServiceDetailsDto> GetEntityByIdAsync(int id);
        Task HardDeleteForEntityTranslation(List<ServiceTranslation> translations);
        Task<List<ServiceDetailsDto>> GetFullServicesForUserOrCompanyOrOffer(List<int> servicesIds, List<int> subservicesIds, List<int> toolIdsForSubServices);
        Task<List<ServiceDto>> GetAllServicesDtosAsync();
        Task<bool> CheckServicesIsCorrect(List<ServiceValuesDto> serviceValues);
        Task<List<ServiceDetailsDto>> GetFullServicesForOffer(List<ServiceValueForOffer> servicesValues);

    }
}
