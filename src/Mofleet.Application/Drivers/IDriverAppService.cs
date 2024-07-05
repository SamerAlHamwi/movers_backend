
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Drivers.Dto;

namespace ClinicSystem.Drivers
{
    public interface IDriversAppService : IMofleetAsyncCrudAppService<DriverDetailsDto, int, LiteDriverDto
        , PagedDriverResultRequestDto,
        CreateDriverDto, UpdateDriverDto>
    {

    }
}
