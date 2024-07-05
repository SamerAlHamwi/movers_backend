using Mofleet.CrudAppServiceBase;
using Mofleet.Domain.Mediators.Dto;
using Mofleet.Mediators.Dto;

namespace Mofleet.Mediators
{
    public interface IMediatorAppService : IMofleetAsyncCrudAppService<MediatorDetailsDto, int, MediatorDetailsDto, PagedMediatiorResultRequestDto,
         CreateMediatorDto, UpdateMediatorDto>
    {

    }
}
