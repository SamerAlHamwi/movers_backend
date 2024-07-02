using Abp.Domain.Services;
using Mofleet.Domain.Regions.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mofleet.Domain.Regions
{
    public interface IRegionManager : IDomainService
    {
        Task<Region> GetEntityByIdAsync(int id);
        Task<LiteRegionDto> GetEntityDtoByIdAsync(int id);
        Task<bool> CheckIfRegionIsExist(List<RegionTranslation> Translations);

        Task<Region> GetLiteEntityByIdAsync(int id);
        Task IsEntityExistAsync(int id);
        Task<List<string>> GetAllRegionNameForAutoComplete(string inputAutoComplete);
    }
}
