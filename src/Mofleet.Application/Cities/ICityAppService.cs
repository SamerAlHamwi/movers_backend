using Mofleet.Cities.Dto;
using Mofleet.CrudAppServiceBase;

namespace Mofleet.Cities
{
    public interface ICityAppService : IMofleetAsyncCrudAppService<CityDetailsDto, int, LiteCityDto, PagedCityResultRequestDto,
        CreateCityDto, UpdateCityDto>
    {

    }
}
