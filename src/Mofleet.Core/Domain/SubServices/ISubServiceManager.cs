using Abp.Domain.Services;
using Mofleet.Domain.SubServices.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.SubServices
{
    public interface ISubServiceManager : IDomainService
    {
        Task<bool> CheckIfSubServiceIsExist(List<SubServiceTranslationDto> Translations);
        Task<SubServiceDetailsDto> GetEntityByIdAsync(int id);
        Task<SubService> GetLiteEntityByIdAsync(int id);

        Task<List<SubService>> GetSubServicesByIdsAsync(List<int> Ids);

        Task<List<SubServiceTranslation>> GetSubServiceTranslationBySubServiceId(int SubServiceId);
        Task HardDeleteForEntityTranslation(List<SubServiceTranslation> translations);
        Task<bool> DeleteSubServiceForServiceBySerivceId(int serivceId);
    }
}