
using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Trucks.Dto;

namespace ClinicSystem.Trucks
{
    public interface ITruckAppService : IMofleetAsyncCrudAppService<TruckDetailsDto, int, LiteTruckDto
        , PagedTruckResultRequestDto,
        CreateTruckDto, UpdateTruckDto>
    {

    }
}
