using Mofleet.Countries.Dto;
using Mofleet.CrudAppServiceBase;

namespace Mofleet.Countries
{
    public interface ICountryAppService : IMofleetAsyncCrudAppService<CountryDetailsDto, int, CountryDto, PagedCountryResultRequestDto,
        CreateCountryDto, UpdateCountryDto>
    {


    }
}
