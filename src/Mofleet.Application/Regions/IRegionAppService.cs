
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Regions.Dto;

namespace ClinicSystem.Regions
{
    public interface IRegionAppService : IMofleetAsyncCrudAppService<RegionDetailsDto, int, LiteRegionDto
        , PagedRegionResultRequestDto,
        CreateRegionDto, UpdateRegionDto>
    {

    }
}
